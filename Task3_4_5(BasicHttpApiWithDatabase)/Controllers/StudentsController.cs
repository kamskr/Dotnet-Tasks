
//using Task3.Models;

using System;
using Microsoft.AspNetCore.Mvc;
using Task3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Task3.Services;
using Task3.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace Task3.Controllers {

    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase {

        // Student student = new Student();
        private readonly IDbService service;

        public StudentsController(IDbService service){
            this.service = service; 
        }

    // [Authorize(Roles, Policy, etc)] this defines what authorization is needed for that end point, if u want to cover all the endpoints, just place it before the whole class
        [HttpGet]
        public IActionResult GetStudents(){
            return Ok(service.GetStudents());
        }

        [HttpPost("add")]
        public IActionResult CreateStudent(Student student) {
            return Ok(service.AddStudent(student));
        }

        [HttpPost("update")]
        public IActionResult UpdateStudent(Student student) {
            return Ok(service.UpdateStudent(student));
        }

        [HttpPost("delete")]
        public IActionResult DeleteStudent(Student student){
            return Ok(service.DeleteStudent(student.IndexNumber));
        }
    }
}

