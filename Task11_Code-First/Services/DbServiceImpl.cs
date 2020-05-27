using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System;
using Task11.Entities;
using Microsoft.EntityFrameworkCore;
using Task11.DTOs.Requests;

namespace Task11.Services
{
    public class DbServiceImpl : IDbService
    {
        private readonly DoctorDbContext context;

        public DbServiceImpl(DoctorDbContext doctorDbContext)
        {
            this.context = doctorDbContext;
        }

        public Doctor AddDoctor(DoctorRequest doctorRequest)
        {
            var doctor = new Doctor { IdDoctor = doctorRequest.IdDoctor, FirstName = doctorRequest.FirstName, LastName = doctorRequest.LastName, Email = doctorRequest.Email };
            context.Add(doctor);
            context.SaveChanges();

            return doctor;
        }
        public IEnumerable<Doctor> GetDoctors()
        {
            return context.Doctors;
        }

        public Doctor UpdateDoctor(DoctorRequest request)
        {
            var doctor = context.Doctors.FirstOrDefault(d => d.IdDoctor == request.IdDoctor);
            if (doctor != null)
            {
                doctor.FirstName = request.FirstName;
                doctor.LastName = request.LastName;
                doctor.Email = request.Email;

                context.Update(doctor);
                context.SaveChanges();

                return doctor;
            }
            else
                return null;
        }

        public string DeleteDoctor(int id)
        {
            var doctor = context.Doctors.FirstOrDefault(d => d.IdDoctor == id);
            if (doctor != null)
            {
                context.Remove(doctor);
                context.SaveChanges();
                return "Doctor successfully deleted";
            }
            else
                return "Doctor not found :(";
        }
    }
}