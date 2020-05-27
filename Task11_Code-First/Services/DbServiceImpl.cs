using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System;
using Task11.Entities;
using Microsoft.EntityFrameworkCore;

namespace Task11.Services
{
    public class DbServiceImpl : IDbService
    {
        private readonly DoctorDbContext context;

        public DbServiceImpl(DoctorDbContext doctorDbContext)
        {
            this.context = doctorDbContext;
        }
        public IEnumerable<Doctor> GetDoctors()
        {
            return context.Doctors;
        }

        // public Doctor AddDoctor(AddDoctorRequest addDoctorRequest)
        // {
        //     var doctor = new Doctor { IdDoctor = request.IdDoctor, FirstName = request.FirstName, LastName = request.LastName, Email = request.Email };
        //     _codeFirstContext.Add(doctor);
        //     _codeFirstContext.SaveChanges();

        //     return doctor;
        // }

        // public bool UpdateStudent(Student student)
        // {
        //     var s = universityAPBDContext.Student.Where(e => e.IndexNumber == student.IndexNumber).First();
        //     s.FirstName = student.FirstName;
        //     s.LastName = student.LastName;
        //     s.BirthDate = student.BirthDate;
        //     s.IdEnrollment = student.IdEnrollment;
        //     s.Password = student.Password;
        //     s.Salt = student.Salt;
        //     s.RefreshToken = student.RefreshToken;
        //     universityAPBDContext.SaveChanges();
        //     return true;
        // }

        // public bool DeleteStudent(string studentId)
        // {
        //     var s = universityAPBDContext.Student.Where(e => e.IndexNumber == studentId).First();
        //     universityAPBDContext.Student.Remove(s);
        //     universityAPBDContext.SaveChanges();
        //     return true;
        // }
    }
}