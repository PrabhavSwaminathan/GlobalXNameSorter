using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GlobalXSort
{
    class NameSorter
    {
        public static void Main(string[] args)
        {
            //checks if no argument, or more than one argument {text file with names} is available 
            if (args == null || args.Length == 0 || args.Length > 1)
            {
                Console.WriteLine("Please specify, not more than one, filename as a parameter.");
                return;
            }
            //Reading all the names stored in the file and storing it in an array
            var logFile = File.ReadAllLines(args[0]);
            //Saving the names as a list
            var logList = new List<string>(logFile);

            /*
             * This method uses the LINQ method orderby and thenby to sort the list of names
             * The orderby method first splits the name of the user using the Split method and sorts by the last name using the .Last() function
             * Then  it splits the name again and sorts by the first name using the .First() function
             * It checks the middle name if someone has a middle name and sorts it alphabetically 
             * Finally it checks the second middle name and sorts it alphabetically
            */
            List<string> sortedUsers = logList.OrderBy(user => user.Split(" ").Last()).
            ThenBy(user => user.Split(" ").First()).
            ThenBy(user => user.Split(" ").Length == 3 ? user.Split(" ")[1] : "").
            ThenBy(user => user.Split(" ").Length == 4 ? user.Split(" ")[2] : "").
            ToList();

            sortedUsers.ForEach(Console.WriteLine);

            //The name of the output file
            var fileName = "sorted-names-list.txt";

            // Creating the file
            var file = new FileStream(fileName, FileMode.OpenOrCreate);

            // Saving the standard output
            var standardOutput = Console.Out;

            // Creating a StreamWriter
            using (var writer = new StreamWriter(file))
            {
                // Setting the new output
                Console.SetOut(writer);
                // Writing output to file
                sortedUsers.ForEach(Console.WriteLine);
                // Changing the ouput again
                Console.SetOut(standardOutput);
            }
        }
    }
}
