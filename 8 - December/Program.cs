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
                string[] unique = dividers[0].Split(" ");
                string[] output = dividers[1].Split(" ");

                foreach (string num in output)
                {
                    whatNumber(num);
                }

                // Parse the input -- currLine

                // Read new line
                currLine = Console.ReadLine();
            }

            // Do something with the input after runtime.
            Console.WriteLine("Result: " + total);
        }

        public static int whatNumber(string num)
        {

            switch (num.Length)
            {
                case 2: return 1; // Checks for num 1
                case 3: return 7; // Checks for num 7
                case 4: return 4; // Checks for num 4
                case 7: return 8; // Checks for num 8
            }

            string sortedNum = String.Concat(num.OrderBy(c => c));

            switch (sortedNum) {
                case "abcefg": return 0;
                case "acdeg": return 2;
                case "acdfg": return 3;
                case "abdfg": return 5;
                case "abdefg": return 6;
                default: return 9;
            }

            // 0 = abcefg
            // 1 = cf
            // 2 = acdeg
            // 3 = acdfg
            // 4 = bcdf
            // 5 = abdfg
            // 6 = abdefg
            // 7 = acf
            // 8 = abcdefg
            // 9 = abcdfg
        }
    }
}
