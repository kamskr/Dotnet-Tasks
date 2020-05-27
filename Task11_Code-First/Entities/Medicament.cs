using System;
using System.Collections.Generic;

namespace Task11.Entities
{
    public class Medicament
    {
        //[Key]
        public int IdMedicament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public ICollection<Prescription_Medicament> Prescription_Medicament { get; set; }
        public Medicament()
        {
            Prescription_Medicament = new HashSet<Prescription_Medicament>();
        }
    }
}
