using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Task2.Models{
    public class ActiveStudiesGroup{
        
 
        [XmlArrayItem("studies", typeof(Student))]
        public HashSet<ActiveStudies> activeStudies;
        public ActiveStudiesGroup(){}
        public ActiveStudiesGroup(HashSet<ActiveStudies> activeStudies){
            this.activeStudies = activeStudies;
        }
    }
}