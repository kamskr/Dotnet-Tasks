using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTOs.Requests
{
    public class EnrollStudentRequest
    { 
        //[RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]

        public string LastName { get; set; }

        public DateTime Birthdate { get; set; }

        [Required]
        public string Studies { get; set; }
    }
}
