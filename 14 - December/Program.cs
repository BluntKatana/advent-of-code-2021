using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public static string polymer;
        public static Dictionary<(char, char), char> insertionRules = new Dictionary<(char, char), char>();
        public static Dictionary<(char, char), long> pairs = new Dictionary<(char, char), long>();
        public static Dictionary<(char, char), long> tempPairs = new Dictionary<(char, char), long>();
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
            foreach (char c in polymer)
                addElem(c, 1);

            // Create the polymer.
            for (int i = 0; i < polymer.Length - 1; i++)
            {
                addPairMain(polymer[i], polymer[i + 1]);
            }

            for (int step = 0; step < maxStep; step++)
            {
                tempPairs = new Dictionary<(char, char), long>();
                foreach ((char c1, char c2) in pairs.Keys)
                {
                    if (insertionRules.ContainsKey((c1, c2))) {
                        long amount = pairs[(c1, c2)];
                        addPairTemp(c1, insertionRules[(c1, c2)], amount);
                        addPairTemp(insertionRules[(c1, c2)], c2, amount);

                        addElem(insertionRules[(c1, c2)], amount);
                    }
                }

                pairs = tempPairs.ToDictionary(e => e.Key, e => e.Value);
            }

            long maxValue = countChars.Values.Max();
            long minValue = countChars.Values.Min();

            Console.WriteLine("Result: " + (maxValue - minValue));
        }

        public static void addElem(char c1, long amount)
        {
            try
            {
                countChars[c1] += amount;
            } catch
            {
                countChars.Add(c1, amount);
            }
        }

        public static void addPairTemp(char c1, char c2, long amount)
        {
            try { tempPairs[(c1, c2)] += amount; }catch { tempPairs.Add((c1, c2), amount); }
        }

        public static void addPairMain(char c1, char c2)
        {
            try { pairs[(c1, c2)] += 1; } catch { pairs.Add((c1, c2), 1); }
        }

        //public static void getCount(char c1, char c2, int step)
        //{
        //    if (step == maxStep)
        //    {
        //        try { countChars[c1] += 1; } catch { countChars.Add(c1, 1); }
        //        return;
        //    }
        //    if (insertionRules.ContainsKey((c1, c2)))
        //    {
        //        getCount(c1, insertionRules[(c1, c2)], step + 1);
        //        getCount(insertionRules[(c1, c2)], c2, step + 1);
        //    }
        //    else
        //    {
        //        countChars[c1] += 1;
        //        countChars[c2] += 1;
        //    }
        //}
    }
}
