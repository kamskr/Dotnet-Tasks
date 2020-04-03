
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
using WebApplication1.DTOs.Requests;
using WebApplication1.DTOs.Responses;

namespace Task3.Controllers {

    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase {

        Student student = new Student();
        private readonly IDbService _dbService;

        public EnrollmentsController(IDbService dbService){
            _dbService = dbService;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request){
            _service.EnrollStudent(request);

            var response = new EnrollStudentResponse();
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
            _service.PromoteStudents(1, "IT");

            return Ok();
        }
    }
}

