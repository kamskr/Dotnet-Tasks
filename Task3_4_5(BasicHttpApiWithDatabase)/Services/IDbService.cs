using System.Collections.Generic;
using Task3.Models;

namespace Task3.Services {
    public interface IDbService {
        public IEnumerable<GetStudentResponse> GetStudents();
        public List<string> GetSemesterEntries(string studentId);
        public bool ExistsIndexNumber(string index);
        
    }
}