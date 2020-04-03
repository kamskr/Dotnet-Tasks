using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTOs.Requests;

namespace WebApplication1.Services
{
    public class SqlServerStudentDbService : IStudentServiceDb
    {
        public void EnrollStudent(EnrollStudentRequest request)
        {
            //1. Validation - OK
            //2. Check if studies exists -> 404
            //3. Check if enrollment exists -> INSERT
            //4. Check if index does not exists -> INSERT/400
            //5. return Enrollment model

            using (var con = new SqlConnection("conString"))
            using (var com = new SqlCommand())
            {

                com.CommandText = "SELECT * FROM Studies WHERE Name=@Name";
                com.Parameters.AddWithValue("Name", request.Studies);
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();

                //2. EXECUTE THE 1 statement
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    tran.Rollback(); ///...
                    //ERROR - 404 - Studies does not exists
                }
                int idStudies = (int)dr["IdStudies"];

                //3.
                com.CommandText = "SELECT * FROM Enrollment WHERE Semester=1 AND IdStudies=@IdStud";
                //...

                //4. ....

                //x.. INSERT Student
                com.CommandText = "INSERT INTO Student(IndexNumber, FirstName, LastName) VALUES (@FirstName, @LastName, .....";
                //...
                com.Parameters.AddWithValue("FistName", request.FirstName);
                //...
                com.ExecuteNonQuery();

                tran.Commit(); //make all the changes in db visible to another users

                ///tran.Rollback();
            }

        }

        public void PromoteStudents(int semester, string studies)
        {
            
        }
    }
}
