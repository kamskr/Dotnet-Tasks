using System;
using Microsoft.AspNetCore.Mvc;
using Task11.Services;
using Task11.Entities;

namespace Task3.Controllers
{

    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {

        // Student student = new Student();
        private readonly IDbService service;

        public DoctorsController(IDbService service)
        {
            this.service = service;
        }
        // [Authorize(Roles, Policy, etc)] this defines what authorization is needed for that end point, if u want to cover all the endpoints, just place it before the whole class
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(service.GetStudents());
        }

        [HttpPost("add")]
        public IActionResult CreateStudent(Student student)
        {
            return Ok(service.AddStudent(student));
        }

        [HttpPost("update")]
        public IActionResult UpdateStudent(Student student)
        {
            return Ok(service.UpdateStudent(student));
        }

        [HttpPost("delete")]
        public IActionResult DeleteStudent(Student student)
        {
            return Ok(service.DeleteStudent(student.IndexNumber));
        }
    }
}
