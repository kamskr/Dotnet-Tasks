using System;
using System.Collections.Generic;
using Task2.Models;

namespace Task2{
    public class CustomComparer : IEqualityComparer<Student>{

        public  bool Equals(Student x, Student y) {
          
            return StringComparer.InvariantCultureIgnoreCase
                .Equals($"{x.IndexNumber}  {x.FirstName} {x.LastName}",
                $"{y.IndexNumber}  {y.FirstName} {y.LastName}");
        }
        
        // override object.GetHashCode
        public int GetHashCode(Student obj) {
            return StringComparer.CurrentCultureIgnoreCase.GetHashCode($"{obj.IndexNumber} {obj.Email} {obj.FirstName} {obj.LastName}");
        }
    }
}