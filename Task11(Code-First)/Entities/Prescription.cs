using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    public class Prescription
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        //[ForeignKey("Doctor")]
        public int IdDoctor { get; set; }
        public virtual Doctor Doctor { get; set; }

    }
}
