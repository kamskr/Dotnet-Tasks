using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task11.Entities
{
    public class Patient
    {
        //[Key]
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }

        public Patient()
        {
            Prescriptions = new HashSet<Prescription>();
        }
    }
}
