using System;
using System.Collections.Generic;
using System.IO;
using Task2.Service;

namespace Task2{
    public class Program {
        public static void Main(string[] args) {
            // path to csv file
            var path = args.Length > 0 ? args[0] : throw new ArgumentNullException();
            //file type
            var inputFileType = args.Length > 0 ? args[1] : throw new ArgumentNullException();
            // type of output file
            var outPutFileType = args.Length > 0 ? args[2] : throw new ArgumentNullException();
            //wrapper for the file
            var fileInfo = new FileInfo(path);

            string logFile = @"log.txt";
            System.IO.File.WriteAllText(logFile,string.Empty);
            
            FileParser fileParser = new FileParser(fileInfo, inputFileType, logFile);
            List<string[]> arrayPersonList = fileParser.ParseFile();
            FileConverter fileConverter = new FileConverter(arrayPersonList, outPutFileType, logFile);
            fileConverter.ConvertFile();

            
        }
    }
}
