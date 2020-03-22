
using System;
using System.Collections.Generic;
using System.IO;

namespace Task2.Service{
    public class FileParser{
        public FileInfo FI{get; set;}
        public string InputFileType {get; set;}
        public string LogFile{get; set;}

        public FileParser(FileInfo FI, string InputFileType, string LogFile){
            this.FI = FI;
            this.InputFileType = InputFileType;
            this.LogFile = LogFile;
        }
        
        public List<string[]> ParseFile(){
            if(String.Equals(InputFileType, "csv")){
                return ParseFileFromCsv();
            }
            else {
                using (System.IO.StreamWriter file = 
                    new System.IO.StreamWriter(this.LogFile, true)){
                        file.WriteLine("ERR: Can't process this file type\n ");
                    }
                return null;
            }
        }

        private List<string[]> ParseFileFromCsv(){
            List<string[]> arrayPersonList = new List<string[]>();
            try{
                using (var stream = new StreamReader(this.FI.OpenRead())){
                    string line = null;
                    while((line = stream.ReadLine()) != null){
                        string[] columns = line.Split(',');
                         arrayPersonList.Add(columns);
                    }
                }
            } catch (ArgumentException){
                using (System.IO.StreamWriter file = 
                    new System.IO.StreamWriter(this.LogFile, true)){
                        file.WriteLine("ERR: The path is incorrect!\n ");
                    }
                Environment.Exit(0);
            } catch (FileNotFoundException){
                using (System.IO.StreamWriter file = 
                    new System.IO.StreamWriter(this.LogFile, true)){
                        file.WriteLine("ERR: The path is incorrect!\n ");
                    }
                Environment.Exit(0);
            }
            return arrayPersonList;
        }
        

    }
}