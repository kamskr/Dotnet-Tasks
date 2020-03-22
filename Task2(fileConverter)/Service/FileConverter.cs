
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;
using Task2.Models;

namespace Task2.Service{
    public class FileConverter{
        public List<string[]> ArrayPersonList{get; set;}

        public string OutputFileType{get; set;}
        public string LogFile{get; set;}
        public FileConverter(List<string[]> ArrayPersonList, string OutputFileType, string LogFile){
            this.ArrayPersonList = ArrayPersonList;
            this.OutputFileType = OutputFileType;
            this.LogFile = LogFile;
        }

        public void ConvertFile(){
            if(String.Equals(OutputFileType, "xml")){
                SerializeToXML();
            }else if(String.Equals(OutputFileType, "json")){
                SerializeToJson();
            }
        }

        private University Convert(){
            var studentList = new HashSet<Student>(new CustomComparer());
            var activeStudies = new HashSet<ActiveStudies>();

            foreach(var row in ArrayPersonList){
                Student student = null;
                string wrongStudent = "";

                if(row.Length == 9 && !row.Any(item => String.Equals(item,""))){
                    student = new Student(row[0],row[1], row[2], row[3], row[4], row[5], row[6], row[7],row[8]);

                    Boolean exists = false;
                    foreach(var activeStudy in activeStudies){
                        if(String.Equals(activeStudy.Name, row[2])) {
                            exists = true;
                            activeStudy.NumberOfStudents++;
                        }
                    }
                    if(!exists){
                        activeStudies.Add(new ActiveStudies(row[2], 1));
                    }
                    
                    if (!studentList.Add(student)){
                        foreach (var item in row){
                            wrongStudent += $" {item}";
                        }
                        using (System.IO.StreamWriter file = 
                            new System.IO.StreamWriter(this.LogFile, true)){
                                file.WriteLine($"ERR: Duplicate student:  {wrongStudent}");
                        }
                        continue;
                    }
                }else{
                    foreach (var item in row){
                        wrongStudent += $" {item}";
                    }
                    using (System.IO.StreamWriter file = 
                        new System.IO.StreamWriter(this.LogFile, true)){
                            file.WriteLine($"ERR: There are missing values in:  {wrongStudent}");
                        }
                }
            }
            var activeStudiesGroup = new ActiveStudiesGroup(activeStudies);
            var studentGroup = new StudentGroup(studentList);
            Console.WriteLine($"Converted {studentList.Count()} students");
            var university = new University(studentGroup, activeStudiesGroup);

            return university;
            //open file writer and create the file
            
        }

        public void SerializeToXML(){
            var university = Convert();
            var parsedDate = DateTime.Parse("2000-02-12");
            FileStream writer = new FileStream(@"result.xml", FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(University),new XmlRootAttribute("university", DateTime=parsedDate));
            serializer.Serialize(writer, university);
        }

        public void SerializeToJson(){
            var university = Convert();

            var jsonString = JsonSerializer.Serialize(university); 
            File.WriteAllText("result.json", jsonString);
        }
    }
}