using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Task2.Models{
    public class University{
        
 
        [XmlArrayItem("student", typeof(Student))] 
        [JsonPropertyName("students")]
        public HashSet<Student> students{get; set;}
        [XmlArrayItem("activeStudies", typeof(ActiveStudies))] 
        [JsonPropertyName("activeStudies")]
        public HashSet<ActiveStudies> activeStudies{get; set;}
        public University(){}
        public University(StudentGroup studentGroup, ActiveStudiesGroup activeStudiesGroup){
            this.students = studentGroup.students;
            this.activeStudies = activeStudiesGroup.activeStudies;
        }
    }
}