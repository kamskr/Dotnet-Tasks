using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Task2.Models{
    public class Student{
        
        [XmlAttribute(attributeName:"index")]
        [JsonPropertyName("indexNumber")]
        public string IndexNumber{get; set;}
        [XmlElement(ElementName = "fname")]
        [JsonPropertyName("fname")]
        public string FirstName{get; set;}
        [XmlElement(ElementName  = "lname")]
        [JsonPropertyName("lname")]
        public string LastName{get; set;}
        [XmlElement(ElementName  = "birthdate")]
        [JsonPropertyName("birthdate")]
        public string BirthDate{get; set;}
        [XmlElement(ElementName  = "email")]
        [JsonPropertyName("email")]
        public string Email{get; set;}
        [XmlElement(ElementName  = "mothersName")]
        [JsonPropertyName("mothersName")]
        public string MothersName{get; set;}
        [XmlElement(ElementName  = "fathersName")]
        [JsonPropertyName("fathersName")]
        public string FathersName{get; set;}
        // [XmlArray("studies")]
        [XmlElement(ElementName  = "studies")]
        [JsonPropertyName("studies")]
        public Studies studies{get; set;}

        public Student(){}
        public Student(string fn, string ln, string sn, string sm, string index, string bd, string email, string motherN, string fatherN){
            IndexNumber = index;
            FirstName = fn;
            LastName = ln;
            BirthDate = bd;
            Email = email;
            MothersName = motherN;
            FathersName = fatherN;
            studies = new Studies(sn, sm);
        }

        
        
    }
}