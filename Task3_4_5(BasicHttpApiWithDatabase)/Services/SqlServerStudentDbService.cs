using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Task3.DTOs.Requests;
using Task3.DTOs.Responses;
using Task3.Models;
using Task3.DTOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using System.Security.Cryptography;

namespace Task3.Services{
    public class SqlServerStudentDbService : IStudentsServiceDb {

        private static SqlConnection _sqlConnection;

        private static void initializeConnection(){
            _sqlConnection = new SqlConnection(@"Server=localhost,1433\\Catalog=UniversityAPBD;Database=UniversityAPBD;User=SA;Password=*;");
        }

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request){
            int IdEnrollment = 0;
            initializeConnection();
            using(_sqlConnection){
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
            initializeConnection();
            using(_sqlConnection){
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
            initializeConnection();
            using(_sqlConnection){
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

        public void StoreRefreshToken(string refreshToken, LoginRequestDto request){
            initializeConnection();
            using(_sqlConnection){
                using(var command = new SqlCommand()){
                        command.CommandText = "UPDATE Student SET RefreshToken = @token WHERE IndexNumber = @index";
                        command.Parameters.AddWithValue("token", refreshToken);
                        command.Parameters.AddWithValue("index", request.Login);
                        command.Connection = _sqlConnection;
                        _sqlConnection.Open();
                        command.ExecuteReader();
                }
            }
        }

        public bool CheckRefreshToken(string refreshToken, LoginRequestDto request) {
            var testToken = "";
            initializeConnection();
            using(_sqlConnection){
                using(var command = new SqlCommand()){
                    command.Connection = _sqlConnection;
                    command.CommandText = "select RefreshToken from Student WHERE IndexNumber = @IndexNumber";
                    command.Parameters.AddWithValue("IndexNumber", request.Login);
                    _sqlConnection.Open();
                    var reader = command.ExecuteReader();
                    while(reader.Read()){
                        testToken = reader["RefreshToken"].ToString();
                    }
                }
            }

            if(testToken == refreshToken && testToken != "") {
                return true;
            }

            return false;
        }

        public bool AuthenticateUser(LoginRequestDto request) {
            string testPassword = "";
            string salt = "";

            initializeConnection();
            using(_sqlConnection){
                using(var command = new SqlCommand()){
                    command.Connection = _sqlConnection;
                    command.CommandText = "select Password, Salt from Student WHERE IndexNumber = @IndexNumber";
                    command.Parameters.AddWithValue("IndexNumber", request.Login);
                    _sqlConnection.Open();
                    var reader = command.ExecuteReader();

                    while(reader.Read()){
                        testPassword = reader["Password"].ToString();
                        salt = reader["Salt"].ToString();
                    }
                }
            }

            // hash test password and compare 
            var valueBytes = KeyDerivation.Pbkdf2(
                password: request.Password,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000, //for server: 40000, for mobile app around 20000, the more iterations, the longer it takes to calculate but its safer
                numBytesRequested: 256 / 8
            );

            var requestPasswordEncrypted = Convert.ToBase64String(valueBytes);

            if(testPassword == requestPasswordEncrypted && testPassword != ""){
                return true;
            }
            return false;
        }

        // private static string CreateSalt(){
        //     byte[] randomBytes = new byte[128/8];
        //     using(var generator = RandomNumberGenerator.Create()) {
        //         generator.GetBytes(randomBytes);
        //         return Convert.ToBase64String(randomBytes);
        //     }

        // }
    }
}
