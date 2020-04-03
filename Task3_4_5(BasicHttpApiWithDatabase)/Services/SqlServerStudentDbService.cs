using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTOs.Requests;

namespace Task3.Services{
    public class SqlServerStudentDbService : IStudentsServiceDb {

        private static SqlConnection _sqlConnection;
        static SqlServerStudentDbService() {
           _sqlConnection = new SqlConnection(@"Server=localhost,1433\\Catalog=UniversityAPBD;Database=UniversityAPBD;User=SA;Password=RGFIsland1738;"); // I was using my local mssql server
        }
        public void EnrollStudent(EnrollStudentRequest request){
            //1. Validation - OK
            //2. Check if studies exists -> 404
            //3. Check if enrollment exists -> INSERT
            //4. Check if index does not exists -> INSERT/400
            //5. return Enrollment model
            
            using(_sqlConnection){//connection string, you have to find yours
                using(var command = new SqlCommand()){
                    command.CommandText = "SELECT * FROM Enrollment,Studies WHERE Enrollment.IdStudy = Studies.IdStudy AND Enrollment.Semester=1 AND Studies.Name=@IdStud ORDER BY Enrollment.StartDate";
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
                        com.Parameters.AddWithValue("IdEnrollment",rnd.Next(10,1000));
                        com.Parameters.AddWithValue("Today",DateTime.Now);
                        com.ExecuteNonQuery();
                    }else{
                        Console.Write("Exists");
                    }
                    // while(reader.Read()){
                    //     Console.Write(reader.GetValue(5));
                    // }
                    tran.Commit();
                }
            }



            // using (var com = new SqlCommand()){

                

            //     _sqlConnection.Open();
            //     var tran = _sqlConnection.BeginTransaction();

            //     //2. EXECUTE THE 1 statement
            //     var dr = com.ExecuteReader();
            //     if (!dr.Read())
            //     {
            //         tran.Rollback(); ///...
            //         //ERROR - 404 - Studies does not exists
            //     }
            //     int idStudies = (int)dr["IdStudies"];

            //     //3.
            //     com.CommandText = "SELECT * FROM Enrollment WHERE Semester=1 AND IdStudies=@IdStud";
            //     //...

            //     //4. ....

            //     //x.. INSERT Student
            //     com.CommandText = "INSERT INTO Student(IndexNumber, FirstName, LastName) VALUES (@FirstName, @LastName, .....";
            //     //...
            //     com.Parameters.AddWithValue("FistName", request.FirstName);
            //     //...
            //     com.ExecuteNonQuery();

            //     tran.Commit(); //make all the changes in db visible to another users

            //     ///tran.Rollback();
            // }

        }

        public void PromoteStudents(int semester, string studies){
            
        }
    }
}
