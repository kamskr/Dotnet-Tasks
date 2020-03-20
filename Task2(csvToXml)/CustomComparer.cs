using System;
using System.Collections.Generic;
using Task2_csvToXml.Models;

namespace Task2_csvToXml{
    public class CustomComparer : IEqualityComparer<Student>{

        public  bool Equals(Student x, Student y) {
          
            return StringComparer.InvariantCultureIgnoreCase
            .Equals($"{x.IndexNumber} {x.Email} {x.FirstName} {x.LastName}",$"{y.IndexNumber} {y.Email} {y.FirstName} {y.LastName}");

        }
        
        // override object.GetHashCode
        public int GetHashCode(Student obj) {
            return StringComparer.CurrentCultureIgnoreCase.GetHashCode($"{obj.IndexNumber} {obj.Email} {obj.FirstName} {obj.LastName}");
        }
    }
}