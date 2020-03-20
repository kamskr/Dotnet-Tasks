using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Task2_csvToXml.Models{
    public class Student{
        
        [XmlAttribute(attributeName:"index")]
        public string IndexNumber{get; set;}
        // [XmlElement(elementName="email-address")]
        public string Email{get; set;}
        public string FirstName{get; set;}
        private string _lastName;
        public string LastName{
            get { return _lastName; }
            set {
                _lastName = value ?? throw new System.ArgumentException();//if its null it will throw exception
            }
        }
    }
}