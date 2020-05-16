using System.Collections.Generic;
using Task3.Models;
using Microsoft.Data.SqlClient;
using System;
using Task3.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Task3.Services {
    public class MockDbService : IDbService {
        private readonly UniversityAPBDContext universityAPBDContext;

        public MockDbService(UniversityAPBDContext universityAPBDContext){
            this.universityAPBDContext = universityAPBDContext; 
        }
        public IEnumerable<GetStudentResponse> GetStudents() {
             var students = universityAPBDContext.Student.Include(x => x.IdEnrollmentNavigation).ThenInclude(x => x.IdStudyNavigation).Select(student => new GetStudentResponse{
                IndexNumber = student.IndexNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate.ToShortDateString(),
                Semester = student.IdEnrollmentNavigation.IdEnrollment,
                Studies = student.IdEnrollmentNavigation.IdStudyNavigation.Name
            }).ToList();
            return students;
        }

        public List<string> GetSemesterEntries(string studentId){
            // initializeConnection();
            // var semesterEntries = new List<string>();
            // using(_sqlConnection){//connection string, you have to find yours
            //     using(var command = new SqlCommand()){
            //         command.Connection = _sqlConnection;
            //         command.CommandText = "select Semester from Enrollment, Student where Student.IdEnrollment = Enrollment.IdEnrollment AND Student.IndexNumber = @studentId;";
            //         command.Parameters.AddWithValue("studentId", studentId);
            //         _sqlConnection.Open();
            //         var reader = command.ExecuteReader();
            //         while(reader.Read()){
            //             semesterEntries.Add(reader["Semester"].ToString());
            //         }
            //     }
            // }
            // return semesterEntries;
            return null;
        }

        public bool ExistsIndexNumber(string index){
            // sql connection
            // // select count(1) from Student where IndexNumber = index... sth like that
            // // var numberOfStudents = int.Parse(dr.Read().ToString()):
            // // if one -> return true, else false
            // int counter = 0;
            // initializeConnection();
            // using(_sqlConnection){//connection string, you have to find yours
            //     using(var command = new SqlCommand()){
            //         command.Connection = _sqlConnection;
            //         command.CommandText = "select count(IndexNumber) FROM Student where IndexNumber = @indexNumber;";
            //         command.Parameters.AddWithValue("indexNumber", index);
            //         _sqlConnection.Open();
            //         counter = int.Parse(command.ExecuteScalar().ToString());
            //     }
            // }
            // if (counter == 0) {
            //     return false;
            // }
            // return true;
            return true;
        }
    }
}