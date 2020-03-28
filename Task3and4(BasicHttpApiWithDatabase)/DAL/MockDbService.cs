using System.Collections.Generic;
using Task3.Models;
using Microsoft.Data.SqlClient;
using System;

namespace Task3.DAL {
    public class MockDbService : IDbService {

        private static IEnumerable<Student> _students;
        
        static MockDbService() {
            var students = new List<Student>();
            using(var sqlConnection = new SqlConnection(@"Server=localhost,1433\\Catalog=UniversityAPBD;Database=UniversityAPBD;User=SA;Password=RGFIsland1738;")){//connection string, you have to find yours
                using(var command = new SqlCommand()){
                    command.Connection = sqlConnection;
                    //
                    command.CommandText = "select s.FirstName, s.LastName, s.BirthDate, st.Name as Studies,e.Semester from Student s join Enrollment e on e.IdEnrollment = s.IdEnrollment join Studies st on st.IdStudy = e.IdStudy";
                    sqlConnection.Open();
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
            _students = students;
        }
        public IEnumerable<Student> GetStudents() {
            return _students;
        }
        
    }
}