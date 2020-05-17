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
using Task3.Entities;

namespace Task3.Services{
    public class SqlServerStudentDbService : IStudentsServiceDb {
        private readonly UniversityAPBDContext universityAPBDContext;

        public SqlServerStudentDbService(UniversityAPBDContext universityAPBDContext){
            this.universityAPBDContext = universityAPBDContext; 
        }

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request){
            Random rnd = new Random();

            var idStudy = rnd.Next(10,1000);

            universityAPBDContext.Studies.Add(new Studies{
                IdStudy = idStudy,
                Name = request.Studies
            });

            universityAPBDContext.Enrollment.Add(new Enrollment{
                IdEnrollment = rnd.Next(10,1000),
                Semester = 1,
                IdStudy = idStudy,  
                StartDate = DateTime.Now
            });

            universityAPBDContext.Student.Add(new Student{
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.Birthdate,
            });

            universityAPBDContext.SaveChanges();

            var response = new EnrollStudentResponse();
            response.Semester = 1;
            response.LastName = request.LastName;
            return response;
        }

        public bool PromoteStudents(int semester, string studies){
            Console.Write("Promoting");
            // universityAPBDContext.ExecuteCommand("EXEC Promote {0}, {1}", studies, semester);
            return true;
        }   


        // public void StoreRefreshToken(string refreshToken, LoginRequestDto request){
        //     initializeConnection();
        //     using(_sqlConnection){
        //         using(var command = new SqlCommand()){
        //                 command.CommandText = "UPDATE Student SET RefreshToken = @token WHERE IndexNumber = @index";
        //                 command.Parameters.AddWithValue("token", refreshToken);
        //                 command.Parameters.AddWithValue("index", request.Login);
        //                 command.Connection = _sqlConnection;
        //                 _sqlConnection.Open();
        //                 command.ExecuteReader();
        //         }
        //     }
        // }

        // public bool CheckRefreshToken(string refreshToken, LoginRequestDto request) {
        //     var testToken = "";
        //     initializeConnection();
        //     using(_sqlConnection){
        //         using(var command = new SqlCommand()){
        //             command.Connection = _sqlConnection;
        //             command.CommandText = "select RefreshToken from Student WHERE IndexNumber = @IndexNumber";
        //             command.Parameters.AddWithValue("IndexNumber", request.Login);
        //             _sqlConnection.Open();
        //             var reader = command.ExecuteReader();
        //             while(reader.Read()){
        //                 testToken = reader["RefreshToken"].ToString();
        //             }
        //         }
        //     }

        //     if(testToken == refreshToken && testToken != "") {
        //         return true;
        //     }

        //     return false;
        // }

        // public bool AuthenticateUser(LoginRequestDto request) {
        //     string testPassword = "";
        //     string salt = "";

        //     initializeConnection();
        //     using(_sqlConnection){
        //         using(var command = new SqlCommand()){
        //             command.Connection = _sqlConnection;
        //             command.CommandText = "select Password, Salt from Student WHERE IndexNumber = @IndexNumber";
        //             command.Parameters.AddWithValue("IndexNumber", request.Login);
        //             _sqlConnection.Open();
        //             var reader = command.ExecuteReader();

        //             while(reader.Read()){
        //                 testPassword = reader["Password"].ToString();
        //                 salt = reader["Salt"].ToString();
        //             }
        //         }
            // }

            // hash test password and compare 
            // var valueBytes = KeyDerivation.Pbkdf2(
            //     password: request.Password,
            //     salt: Encoding.UTF8.GetBytes(salt),
            //     prf: KeyDerivationPrf.HMACSHA512,
            //     iterationCount: 10000, //for server: 40000, for mobile app around 20000, the more iterations, the longer it takes to calculate but its safer
            //     numBytesRequested: 256 / 8
            // );

        //     var requestPasswordEncrypted = Convert.ToBase64String(valueBytes);

        //     if(testPassword == requestPasswordEncrypted && testPassword != ""){
        //         return true;
        //     }
        //     return false;
        // }

        // private static string CreateSalt(){
        //     byte[] randomBytes = new byte[128/8];
        //     using(var generator = RandomNumberGenerator.Create()) {
        //         generator.GetBytes(randomBytes);
        //         return Convert.ToBase64String(randomBytes);
        //     }

        // }
    }
}
