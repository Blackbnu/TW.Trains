using System;
using System.IO;

namespace Trains.Domain.RailRoadServices
{
    public class RailRoadIOService
    {
        public virtual string GetInput()
        {
            var fullPath = GetFullPathInputFile();
            return File.ReadAllText(fullPath);
        }

        private static string GetFullPathInputFile()
        {
            string fullPath;
            do
            {
                Console.WriteLine("Where is the input file?");
                Console.WriteLine("(if there is an input.txt in current location just press Enter)");
                
                fullPath = Console.ReadLine();
                if (string.IsNullOrEmpty(fullPath))
                    fullPath = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

                if (!File.Exists(fullPath))
                {
                    fullPath = string.Empty;
                    Console.Clear();
                    Console.WriteLine("File not found!");
                }
            } while (string.IsNullOrEmpty(fullPath));

            return fullPath;
        }

        public void OutPut(int outputNumber, Func<int> action)
        {
            string result;
            try
            {
                result = action.Invoke().ToString();
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            Console.WriteLine(string.Format("Output #{0}: {1}", outputNumber, result));
        }
    }
}