using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public static string polymer;
        public static Dictionary<(char, char), char> insertionRules = new Dictionary<(char, char), char>();
        public static Dictionary<char, long> countChars;
        public static int maxStep = 40;
        static void Main(string[] args)
        {
            // Parse the input.
            polymer = Console.ReadLine();
            _ = Console.ReadLine();
            string currLine = Console.ReadLine();
            while (currLine != ":q")
            {
                string[] insertion = currLine.Split(" ");
                insertionRules.Add((insertion[0][0], insertion[0][1]), insertion[2][0]);
                currLine = Console.ReadLine();
            }

            // Create the dictionary.
            countChars = polymer.Distinct().ToDictionary(c => c, c => (long)0);
            countChars[polymer.Last()] += 1;
            // Create the polymer.
            for (int i = 0; i < polymer.Length - 1; i++)
            {
                getCount(polymer[i], polymer[i+1], 0);
            }

            long maxValue = countChars.Values.Max();
            long minValue = countChars.Values.Min();

            Console.WriteLine("Result: " + (maxValue - minValue));
        }

        public static void getCount(char c1, char c2, int step)
        {
            if (step == maxStep)
            {
                try { countChars[c1] += 1; } catch { countChars.Add(c1, 1); }
                //try { countChars[c2] += 1; } catch { countChars.Add(c2, 1); }
                return;
            }
            if (insertionRules.ContainsKey((c1, c2)))
            {
                getCount(c1, insertionRules[(c1, c2)], step + 1);
                getCount(insertionRules[(c1, c2)], c2, step + 1);
            }
            else
            {
                countChars[c1] += 1;
                countChars[c2] += 1;
            }
        }
    }
}
