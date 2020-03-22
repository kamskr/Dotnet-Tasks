using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Task2.Models{
    public class ActiveStudies{
        
 
        [XmlAttribute(attributeName: "name")]
        public string Name{get; set;}
        [XmlAttribute(attributeName: "numberOfStudents")]
        public int NumberOfStudents{get; set;}
        public ActiveStudies(){}
        public ActiveStudies(string Name, int NumberOfStudents){
            this.Name = Name;
            this.NumberOfStudents = NumberOfStudents;
        }
    }
}