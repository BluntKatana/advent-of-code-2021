using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {

            // Read the first line of input.
            string currLine = Console.ReadLine();

            int total = 0;

            // Check when all input is read.
            while (currLine != ":q")
            {
                string[] dividers = currLine.Split("|");
                string[] unique = dividers[0].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string[] output = dividers[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var combinedNumbers = new Dictionary<int, string>();
                foreach (string num in output)
                {
                    if (num.Length == 2) combinedNumbers.Add(2, num);
                    if (num.Length == 4) combinedNumbers.Add(4, num);
                    if (num.Length == 3) combinedNumbers.Add(7, num);
                    if (num.Length == 7) combinedNumbers.Add(8, num);
                }

                // Read new line
                currLine = Console.ReadLine();
            }

            // Do something with the input after runtime.
            Console.WriteLine("Result: " + total);
        }
    }
}
