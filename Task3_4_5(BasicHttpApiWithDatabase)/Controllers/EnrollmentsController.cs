
//using Task3.Models;

using System;
using Microsoft.AspNetCore.Mvc;
using Task3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Task3.Services;
using Microsoft.Data.SqlClient;
using Task3.DTOs.Requests;
using Task3.DTOs.Responses;
using Task3.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Task3.Controllers {

    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase {

        Student student = new Student();
        private readonly IStudentsServiceDb _dbService;

        public EnrollmentsController(IStudentsServiceDb dbService){
            _dbService = dbService;
        }

        [Authorize(Roles = "employee")]
        [HttpPost("enroll")]
        public IActionResult EnrollStudent(EnrollStudentRequest request){
            var response = _dbService.EnrollStudent(request);
            return Ok(response);
        }
    
        [Authorize(Roles = "employee")]
        [HttpPost("promote")]
        public IActionResult PromoteStudents(){
            Enrollment enrollment = _dbService.PromoteStudents(1, "IT");
            return Ok(enrollment);
        }

        [HttpPost]
        public IActionResult Login(LoginRequestDto request) {

            var userExists = _dbService.AuthenticateUser(request);

            if(!userExists) {
                return StatusCode(401);
            }
            var refreshTokenValue = Guid.NewGuid();
            var refreshAccessToken = refreshTokenValue.ToString();

            _dbService.StoreRefreshToken(refreshAccessToken, request);

            return Ok(new {
                token = new JwtSecurityTokenHandler().WriteToken(CreateAccesToken()),
                refreshToken = refreshAccessToken // saved on client side and database
            });
        }

        //endpoint for refreshing the token
         [HttpPost("refresh-token/{refreshToken}")]
        public IActionResult RefreshToken(string refreshToken, LoginRequestDto request) {
            //check in db if refresh token exists
            Console.Write(refreshToken);
            if(_dbService.CheckRefreshToken(refreshToken, request)){
                var refreshTokenValue = Guid.NewGuid();
                var refreshAccessToken = refreshTokenValue.ToString();
                _dbService.StoreRefreshToken(refreshAccessToken, request);
                return Ok(new {
                token = new JwtSecurityTokenHandler().WriteToken(CreateAccesToken()),
                //store this refresh token in the database
                refreshToken = Guid.NewGuid() // saved on client side and database
                });
            } else {
                return StatusCode(401);
            }
        }

        private JwtSecurityToken CreateAccesToken(){
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"), //student id, sth that identifies
                new Claim(ClaimTypes.Name, "bob123"),
                new Claim(ClaimTypes.Role, "employee"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asidnvasdiuhvasdvaspoih"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken (
                issuer: "Kamil",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
            );
            return token;
        }

    }
}

