using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Task2.Models{
    public class Studies{
        
 
        [XmlElement(ElementName  = "name")]
        public string StudiesName{get; set;}
        [XmlElement(ElementName  = "mode")]
        public string StudiesMode{get; set;}

        public Studies(){}
        public Studies(string sn, string sm){
            StudiesName = sn;
            StudiesMode = sm;
        }
    }
}