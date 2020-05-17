using System.Collections.Generic;
using System.Linq;
using Task3.Models;
using Microsoft.Data.SqlClient;
using System;
using Task3.Entities;
using Microsoft.EntityFrameworkCore;

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

        public bool AddStudent(Student student){
            universityAPBDContext.Student.Add(student);
            universityAPBDContext.SaveChanges();
            return true;
        }

        public bool UpdateStudent(Student student) {
            var s = universityAPBDContext.Student.Where(e => e.IndexNumber == student.IndexNumber).First();
            s.FirstName = student.FirstName;
            s.LastName = student.LastName;
            s.BirthDate = student.BirthDate;
            s.IdEnrollment = student.IdEnrollment;
            s.Password = student.Password;
            s.Salt = student.Salt;
            s.RefreshToken = student.RefreshToken;
            universityAPBDContext.SaveChanges();
            return true;
        }

        public bool DeleteStudent(string studentId) {
            var s = universityAPBDContext.Student.Where(e => e.IndexNumber == studentId).First();
            universityAPBDContext.Student.Remove(s);
            universityAPBDContext.SaveChanges();
            return true;
        }
    }
}