using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Task2_csvToXml.Models;

namespace Task2_csvToXml {
    public class Program {
        public static void Main(string[] args) {
            // path to csv file
            var path = @"Data/dane.csv";
            //wrapper for the file
            var fi = new FileInfo(path);

            //stream to read the data from the file
            using (var stream = new StreamReader(fi.OpenRead())){
                string line = null;
                while((line = stream.ReadLine()) != null){
                    string[] columns = line.Split(',');
                }
            }

            var list = new HashSet<Student>(new CustomComparer());
            var st = new Student{
                IndexNumber = " s12345",
                Email = "test@test.com",
                FirstName = "Bob",
                LastName = "Brown"
            };
            var st2 = new Student{
                IndexNumber = " s12345",
                Email = "test@test.com",
                FirstName = "Bob",
                LastName = "Brown"
            };

            list.Add(st);
            if (!list.Add(st2)){
                Console.WriteLine("Duplicate!");
            }

            //open file writer and create the file
            FileStream writer = new FileStream(@"result.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(HashSet<Student>),new XmlRootAttribute("university"));
            serializer.Serialize(writer, list);
        }
    }
}
