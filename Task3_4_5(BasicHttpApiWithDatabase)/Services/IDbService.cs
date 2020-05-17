using System.Collections.Generic;
using Task3.Models;
using Task3.Entities;

namespace Task3.Services {
    public interface IDbService {
        public IEnumerable<GetStudentResponse> GetStudents();
        public bool AddStudent(Student student);
        public bool UpdateStudent(Student student);
        public bool DeleteStudent(string idStudent);
        // public bool ExistsIndexNumber(string index);
        
    }
}