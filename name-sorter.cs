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
            if (args == null || args.Length == 0 || args.Length > 1)
                {
                    Console.WriteLine("Please specify, not more than one, filename as a parameter.");
                    return;
                }

            var logFile = File.ReadAllLines(args[0]);
            var logList = new List<string>(logFile);
            
            List<string> sortedUsers = logList.OrderBy(user => user.Split(" ").Last()).
            ThenBy(user => user.Split(" ").First()).
            ThenBy(user => user.Split(" ")[1]).
            ThenBy(user => user.Split(" ").Length == 4 ? user.Split(" ")[2]: "").
            ToList();

            sortedUsers.ForEach(Console.WriteLine);

            //1. Get the name of the output file
            var fileName = "sorted-names-list.txt";
 
            //2. Create the file
            var file = new FileStream(fileName, FileMode.OpenOrCreate);
 
            //3. Save the standard output
            var standardOutput = Console.Out;
 
            //5. Create a StreamWriter
            using (var writer = new StreamWriter(file))
            {
                //6. Set the new output
                Console.SetOut(writer);
                //7. Write something
                sortedUsers.ForEach(Console.WriteLine);
                //8. Change the ouput again
                Console.SetOut(standardOutput);
            }
        }
    }
} 