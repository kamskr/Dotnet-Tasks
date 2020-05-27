using System;
using Microsoft.AspNetCore.Mvc;
using Task11.Services;
using Task11.Entities;
using Task11.DTOs.Requests;

namespace Task11.Controllers
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
        public IActionResult GetDoctor()
        {
            return Ok(service.GetDoctors());
        }

        [HttpPost("add")]
        public IActionResult AddDoctor(DoctorRequest doctor)
        {
            return Ok(service.AddDoctor(doctor));
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteDoctor(int id)
        {
            return Ok(service.DeleteDoctor(id));
        }

        [HttpPut]
        public IActionResult UpdateDoctor(DoctorRequest request)
        {
            return Ok(service.UpdateDoctor(request));
        }
    }
}
