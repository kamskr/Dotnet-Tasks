using System;

namespace Task3.Models {
    public class GetStudentResponse {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set;}
        public int Semester { get; set; }
        public string Studies { get; set; }
    }
}