
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

namespace Task3.Controllers {

    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase {

        Student student = new Student();
        private readonly IStudentsServiceDb _dbService;

        public EnrollmentsController(IStudentsServiceDb dbService){
            _dbService = dbService;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request){
            

            var response = _dbService.EnrollStudent(request);
            return Ok(response);
        }
    
        [HttpPost("promote")]
        public IActionResult PromoteStudents(){
            //Request - name of studies=IT, semester=1

            //1. Check if studies exists
            //2. Find all the students from studies=IT and semester=1
            //3. Promote all students to the 2 semester
            //   Find an enrollment record with studies=IT and semester=2    -> IdEnrollment=10
            //   Update all the students
            //   If Enrollment does not exist -> add new one

            //Create stored procedure
            Enrollment enrollment = _dbService.PromoteStudents(1, "IT");

            return Ok(enrollment);
        }
    }
}

