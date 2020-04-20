
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
using Microsoft.AspNetCore.Authorization;

namespace Task3.Controllers {

    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase {

        Student student = new Student();
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService){
            _dbService = dbService;
        }

    // [Authorize(Roles, Policy, etc)] this defines what authorization is needed for that end point, if u want to cover all the endpoints, just place it before the whole class
        [HttpGet]
        public IActionResult GetStudents(){
            return Ok(_dbService.GetStudents());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(string id) {
            return Ok(_dbService.GetSemesterEntries(id));
        }

        // [HttpPost]
        // public IActionResult CreateStudent(Student student) {
        //     student.IndexNumber = $"s{new Random().Next(1, 20000)}";
        //     return Ok(student);
        // }

        // [HttpPut("{id}")]
        // public IActionResult PutStudent(int id) {
        //     //put is used to update
        //     Console.WriteLine("Updating student student");
        //     student.IdStudent = id;
        //     return Ok("Student Updated!");
        // }

        // [HttpDelete("{id}")]
        // public IActionResult DeleteStudent(int id) {
        //     //put is used to update
        //     if(student.IdStudent == id){
        //         student = null;
        //     }
        //     return Ok("Student Deleted!");
        // }
    }
}

