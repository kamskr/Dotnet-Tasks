using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Task2.Models{
    public class StudentGroup{
        
 
        [XmlArrayItem("student", typeof(Student))]
        public HashSet<Student> students;
        public StudentGroup(){}
        public StudentGroup(HashSet<Student> students){
            this.students = students;
        }
    }
}