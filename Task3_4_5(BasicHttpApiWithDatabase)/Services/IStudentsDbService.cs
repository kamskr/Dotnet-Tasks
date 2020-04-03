using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTOs.Requests;

namespace WebApplication1.Services
{
    public interface IStudentServiceDb
    {
        void EnrollStudent(EnrollStudentRequest req);
        void PromoteStudents(int semester, string studies);
    }
}
