using System.Collections.Generic;
using Task3.Models;

namespace Task3.DAL {
    public interface IDbService {
        public IEnumerable<Student> GetStudents();
        public List<string> GetSemesterEntries(string studentId);
        
    }
}