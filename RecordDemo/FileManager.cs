using System;
using System.Text.RegularExpressions;

namespace FileManager
{
	public class FileManager
	{
        //public String ID { get; set; }
        //public String FirstName { get; set; }
        //public String LastName { get; set; }
        public static readonly string filePath = @"file.txt";
        public FileManager()
		{
					
		
		}

        public bool DeleteFile()
        {
            //    string filePath = @"file.txt";
            

         
            // Check if the file exists before attempting to delete it
            if (File.Exists(filePath))
            {
                // Delete the file
                File.Delete(filePath);
            }

            return true;
           }

		public bool AddRecord( String rec )
		{
            string filePath = @"file.txt";


            String pattern = ",";
            
            MatchCollection matches = Regex.Matches(rec, pattern);

            // Check if there are exactly 2 matches (commas)
            if (matches.Count != 2)
            {
                Console.WriteLine("Invalid record;");
                return false;
                
            }
            
            //  file does not exist
            if (File.Exists(filePath) == false)
            {

                File.WriteAllText(filePath, rec + "\n");
                return true;
            }

            // file exists

            // check duplicate
            string[] parts = rec.Split(',');
            string ID = parts[0];


            string[] lines = File.ReadAllLines(filePath);

            // Check if any line contains the input_ID in the first column (assuming ID is in the first column)
            bool recordExists = lines.Any(line => line.Split(',')[0].Trim() == ID);
            
            if (recordExists)
            {
                Console.WriteLine("Record exists");
                return false;
                
            }


            File.AppendAllText(filePath, rec +"\n");
            return true;
		}
        public bool ReadRec(String id)
        {
            return false;
        }
        

    }
}

