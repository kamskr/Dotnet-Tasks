using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTOs.Requests;

namespace Task3.Services
{
    public interface IStudentsServiceDb
    {
        void EnrollStudent(EnrollStudentRequest req);
        void PromoteStudents(int semester, string studies);
    }
}
