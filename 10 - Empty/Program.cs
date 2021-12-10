using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public static Dictionary<char, char> charPairs    = new Dictionary<char, char>()  { { ')','(' },  { ']', '[' },   { '}', '{' },   { '>', '<' } };
        public static Dictionary<char, int> charCostPart1 = new Dictionary<char, int>()   { { ')', 3 },   { ']', 57 },    { '}', 1197 },  { '>', 25137 } };
        public static Dictionary<char, int> charCostPart2 = new Dictionary<char, int>()   { { '(', 1 },   { '[', 2 },     { '{', 3 },     { '<', 4 } };

        static void Main(string[] args)
        {
            List<long> totalScores = new List<long>();
            string currLine = Console.ReadLine();
            
            while (currLine != ":q")
            {
                string line = currLine;
                // Remove legal chunks from line.
                string[] legalChunks = new[] { "()", "[]", "{}", "<>" };
                while (legalChunks.Any(s => line.Contains(s)))
                    line = line.Replace("()", "").Replace("[]", "").Replace("{}", "").Replace("<>", "");

                // Check for corrupted lines.
                if (checkWrongCharPoints(line) == 0)
                    totalScores.Add(getScore(line));

                // Read new line.
                currLine = Console.ReadLine();
            }

            // Print result.
            totalScores.Sort();
            Console.WriteLine("Result: " + totalScores[totalScores.Count / 2]);
        }

        public static long getScore(string line)
        {
            long totalScore = 0;
            foreach (char c in line.Reverse())
                totalScore = totalScore * 5 + charCostPart2[c];

            return totalScore;
        }

        public static int checkWrongCharPoints(string line)
        {
            for (int i = 1; i < line.Length; i++)
            {
                char prevChar = line[i - 1]; char currChar = line[i]; 
                if (!charPairs.ContainsKey(prevChar) && charPairs.ContainsKey(currChar))
                    if (charPairs[currChar] != prevChar)
                        return charCostPart1[currChar];
            }
            return 0;
        }
    }
}
