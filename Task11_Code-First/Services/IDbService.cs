using System.Collections.Generic;
using Task11.DTOs.Requests;
using Task11.Entities;

namespace Task11.Services
{
    public interface IDbService
    {
        public Doctor AddDoctor(DoctorRequest addDoctorRequest);
        public IEnumerable<Doctor> GetDoctors();
        public Doctor UpdateDoctor(DoctorRequest request);
        public string DeleteDoctor(int id);
    }
}