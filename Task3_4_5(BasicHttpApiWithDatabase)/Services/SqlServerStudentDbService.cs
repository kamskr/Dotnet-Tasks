using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Task3.DTOs.Requests;
using Task3.DTOs.Responses;
using Task3.Models;

namespace Task3.Services{
    public class SqlServerStudentDbService : IStudentsServiceDb {

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request){
            //1. Validation - OK
            //2. Check if studies exists -> 404
            //3. Check if enrollment exists -> INSERT
            //4. Check if index does not exists -> INSERT/400
            //5. return Enrollment model
            int IdEnrollment = 0;
            using(var _sqlConnection = new SqlConnection(@"Server=localhost,1433\\Catalog=UniversityAPBD;Database=UniversityAPBD;User=SA;Password=*****;")){//connection string, you have to find yours
                using(var command = new SqlCommand()){
                    command.CommandText = "SELECT TOP 1  * FROM Enrollment,Studies WHERE Enrollment.IdStudy = Studies.IdStudy AND Enrollment.Semester=1 AND Studies.Name=@IdStud ORDER BY Enrollment.StartDate";
                    command.Parameters.AddWithValue("IdStud", request.Studies);
                    command.Connection = _sqlConnection;
                    
                    _sqlConnection.Open();
                    var tran = _sqlConnection.BeginTransaction();
                    command.Transaction = tran;
                    var reader = command.ExecuteReader();
                    if(!reader.HasRows){
                        reader.Close();
                        Random rnd = new Random();
                        SqlCommand com = new SqlCommand("Insert INTO Studies VALUES (@IdStudy, @StudyName); INSERT INTO Enrollment VALUES (@IdEnrollment, 1, @IdStudy, @Today);", _sqlConnection,tran);
                        com.Parameters.AddWithValue("IdStudy",rnd.Next(10,1000));
                        com.Parameters.AddWithValue("StudyName",request.Studies);
                        IdEnrollment = rnd.Next(10,1000);
                        com.Parameters.AddWithValue("IdEnrollment",IdEnrollment);
                        com.Parameters.AddWithValue("Today",DateTime.Now);
                        com.ExecuteNonQuery();
                    }else{
                        while(reader.Read()){
                            IdEnrollment = (int)reader["IdEnrollment"];
                        }
                        Console.Write("Enrollment Exists");
                    }
                    reader.Close();
                    tran.Commit();
                }
            }
            using(var _sqlConnection = new SqlConnection(@"Server=localhost,1433\\Catalog=UniversityAPBD;Database=UniversityAPBD;User=SA;Password=*****;")){
                using(var command = new SqlCommand()){
                        command.CommandText = "Select * FROM Student where IndexNumber = @IndexNumber";
                        command.Parameters.AddWithValue("IndexNumber", request.IndexNumber);
                        command.Connection = _sqlConnection;
                        
                        _sqlConnection.Open();
                        var tran = _sqlConnection.BeginTransaction();
                        command.Transaction = tran;
                        var reader = command.ExecuteReader();
                        if(!reader.HasRows){
                            reader.Close();
                            Random rnd = new Random();
                            SqlCommand com = new SqlCommand("Insert INTO Student VALUES (@IndexNumber, @FirstName, @LastName, @BirthDate, @IdEnrollment)");
                            com.Parameters.AddWithValue("IndexNumber",request.IndexNumber);
                            com.Parameters.AddWithValue("FirstName",request.FirstName);
                            com.Parameters.AddWithValue("LastName",request.LastName);
                            com.Parameters.AddWithValue("BirthDate",request.Birthdate);
                            com.Parameters.AddWithValue("IdEnrollment",IdEnrollment);
                            com.Connection = _sqlConnection;
                            com.Transaction = tran;
                            com.ExecuteNonQuery();
                        }else{
                            throw new Exception();
                        }
                    
                        tran.Commit();
                }
            }

            var response = new EnrollStudentResponse();
            response.Semester = 1;
            response.LastName = request.LastName;
            return response;
        }

        public Enrollment PromoteStudents(int semester, string studies){
            Console.Write("Promoting");
            var enrollment = new Enrollment();
            using(var _sqlConnection = new SqlConnection(@"Server=localhost,1433\\Catalog=UniversityAPBD;Database=UniversityAPBD;User=SA;Password=*****;")){//connection string, you have to find yours
                using(var command = new SqlCommand()){
                    command.CommandText = "EXEC Promote @Name, @Semester";
                    command.Parameters.AddWithValue("Semester", semester);
                    command.Parameters.AddWithValue("Name", studies);
                    command.Connection = _sqlConnection;
                    
                    _sqlConnection.Open();
                    var tran = _sqlConnection.BeginTransaction();
                    command.Transaction = tran;
                    var reader = command.ExecuteNonQuery();

                    tran.Commit();
                }
            }

            enrollment.Semester = semester + 1;
            enrollment.StudyName = studies;
            return enrollment;
        }
    }
}
