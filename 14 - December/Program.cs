using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public static string polymer;
        public static Dictionary<string, string> insertionRules = new Dictionary<string, string>();
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
                insertionRules.Add(insertion[0], insertion[2]);
                currLine = Console.ReadLine();
            }

            // Create the dictionary.
            countChars = polymer.Distinct().ToDictionary(c => c, c => (long)0);
            countChars[polymer.Last()] += 1;
            // Create the polymer.
            getCount(polymer, 0);

            long maxValue = countChars.Values.Max();
            long minValue = countChars.Values.Min();

            Console.WriteLine("Result: " + (maxValue - minValue));
        }

        public static void getCount(string substring, int step)
        {
            // Count the elements in the substring if maxStep has been passed.
            if (step == maxStep) {
                if (substring.Length > 2)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        try
                        {
                            countChars[substring[i]] += 1;
                        }
                        catch
                        {
                            countChars.Add(substring[i], 1);
                        }
                    }
                }
                else
                {
                    foreach (char c in substring)
                    {
                        try
                        {
                            countChars[c] += 1;
                        }
                        catch
                        {
                            countChars.Add(c, 1);
                        }

                    }
                }
                return;
            }
            else {
                for (int i = 0; i < substring.Length - 1; i++)
                {
                    string checkString = substring[i] + "" + substring[i + 1];
                    if (insertionRules.ContainsKey(checkString))
                        getCount(substring[i] + insertionRules[checkString] + substring[i + 1], step + 1);
                    else
                        getCount(checkString, step + 1);

                }
            }
        }
    }
}
