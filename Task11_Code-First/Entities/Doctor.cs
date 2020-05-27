using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task11.Entities
{
    public class Doctor
    {
        //[Key]
        public int IdDoctor { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }

        public Doctor()
        {
            Prescriptions = new HashSet<Prescription>();
        }
    }
}
