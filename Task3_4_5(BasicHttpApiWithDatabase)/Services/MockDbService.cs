using System.Collections.Generic;
using Task3.Models;
using Microsoft.Data.SqlClient;
using System;

namespace Task3.Services {
    public class MockDbService : IDbService {

        private static SqlConnection _sqlConnection;
        static MockDbService() {
            // I was using my local mssql server
            initializeConnection();
        }

        private static void initializeConnection(){
            _sqlConnection = new SqlConnection(@"Server=localhost,1433\\Catalog=UniversityAPBD;Database=UniversityAPBD;User=SA;Password=*****;");
        }
        public IEnumerable<Student> GetStudents() {
            var students = new List<Student>();
            initializeConnection();
            using(_sqlConnection){//connection string, you have to find yours
                using(var command = new SqlCommand()){
                    command.Connection = _sqlConnection;
                    command.CommandText = "select s.FirstName, s.LastName, s.BirthDate, st.Name as Studies,e.Semester from Student s join Enrollment e on e.IdEnrollment = s.IdEnrollment join Studies st on st.IdStudy = e.IdStudy";
                    _sqlConnection.Open();
                    var reader = command.ExecuteReader();

                    while(reader.Read()){
                        var st = new Student();
                        st.FirstName = reader["FirstName"].ToString();
                        st.LastName = reader["LastName"].ToString();
                        st.BirthDate = DateTime.Parse(reader["BirthDate"].ToString());
                        st.Studies = reader["Studies"].ToString();
                        st.Semester = int.Parse(reader["Semester"].ToString());
                        students.Add(st);
                    }
                }
            }
            return students;
        }

        public List<string> GetSemesterEntries(string studentId){
            initializeConnection();
            var semesterEntries = new List<string>();
            using(_sqlConnection){//connection string, you have to find yours
                using(var command = new SqlCommand()){
                    command.Connection = _sqlConnection;
                    command.CommandText = "select Semester from Enrollment, Student where Student.IdEnrollment = Enrollment.IdEnrollment AND Student.IndexNumber = @studentId;";
                    command.Parameters.AddWithValue("studentId", studentId);
                    _sqlConnection.Open();
                    var reader = command.ExecuteReader();
                    while(reader.Read()){
                        semesterEntries.Add(reader["Semester"].ToString());
                    }
                }
            }
            return semesterEntries;
        }

        public bool ExistsIndexNumber(string index){
            // sql connection
            // select count(1) from Student where IndexNumber = index... sth like that
            // var numberOfStudents = int.Parse(dr.Read().ToString()):
            // if one -> return true, else false
            int counter = 0;
            initializeConnection();
            using(_sqlConnection){//connection string, you have to find yours
                using(var command = new SqlCommand()){
                    command.Connection = _sqlConnection;
                    command.CommandText = "select count(IndexNumber) FROM Student where IndexNumber = @indexNumber;";
                    command.Parameters.AddWithValue("indexNumber", index);
                    _sqlConnection.Open();
                    counter = int.Parse(command.ExecuteScalar().ToString());
                }
            }
            if (counter == 0) {
                return false;
            }
            return true;
        }
    }
}