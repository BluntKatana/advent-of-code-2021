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
                Dictionary<int, string> mapping = new Dictionary<int, string>();

                string[] dividers = currLine.Split("|");
                List<string> wires = dividers[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
                string[] output = dividers[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                // Check numbers 1, 4, 3, 7
                mapping.Add(1, wires.Find(currS => currS.Length == 2));
                mapping.Add(4, wires.Find(currS => currS.Length == 4));
                mapping.Add(7, wires.Find(currS => currS.Length == 3));
                mapping.Add(8, wires.Find(currS => currS.Length == 7));

                // Check numbers 9, 0, 6
                mapping.Add(9, wires.Find(currS => currS.Length == 6 && CompareTwo(currS, mapping[4])));
                mapping.Add(0, wires.Find(currS => currS.Length == 6 && currS != mapping[9] && CompareTwo(currS, mapping[7])));
                mapping.Add(6, wires.Find(currS => currS.Length == 6 && currS != mapping[9] && currS != mapping[0]));

                // Check numbers 3, 2, 5
                mapping.Add(3, wires.Find(currS => currS.Length == 5 && CompareTwo(currS, mapping[1])));
                mapping.Add(5, wires.Find(currS => currS.Length == 5 && CompareTwo(mapping[6], currS)));
                mapping.Add(2, wires.Find(currS => currS.Length == 5 && currS != mapping[5] && currS != mapping[3]));

                // Calculate the number
                int tempTotal = 0;
                for (int i = 0; i < output.Length; i++)
                    for (int j = 0; j < 10; j++)
                        if (CompareTwoExact(output[i], mapping[j]))
                            tempTotal += (int)Math.Pow(10, 3-i) * j;
                total += tempTotal;

                // Read new line
                currLine = Console.ReadLine();
            }

            // Print total
            Console.WriteLine("Result: " + total);
        }

        public static bool CompareTwo(string currString, string checkAgainst)
        {
            return checkAgainst.All(e => currString.Contains(e));
        }

        public static bool CompareTwoExact(string currString, string checkAgainst)
        {
            string newCurr = String.Concat(currString.OrderBy(c => c));
            string newAgainst = String.Concat(checkAgainst.OrderBy(c => c));

            return newCurr == newAgainst;

        }

        // acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf
    }
}
