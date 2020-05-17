using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task3.DTOs;
using Task3.DTOs.Requests;
using Task3.DTOs.Responses;

namespace Task3.Services
{
    public interface IStudentsServiceDb
    {
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest req);
        bool PromoteStudents(int semester, string studies);
        // public void StoreRefreshToken(string refreshToken, LoginRequestDto request);
        // public bool AuthenticateUser(LoginRequestDto request);
        // public bool CheckRefreshToken(string refreshToken,  LoginRequestDto request);
    }
}
