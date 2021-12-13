using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public static List<(int, int)> coords = new List<(int, int)>();
        public static List<(string, int)> folds = new List<(string, int)>();
        static void Main(string[] args)
        {
            // Parse coords.
            string currLine = Console.ReadLine();
            while (currLine != "")
            {
                int[] coord = currLine.Split(',').Select(v => int.Parse(v)).ToArray();
                coords.Add((coord[0], coord[1]));
                currLine = Console.ReadLine();
            }
            // Parse folds.
            while (currLine != ":q")
            {
                if (currLine != "")
                {
                    string[] fold = currLine.Split(" ")[2].Split("=");
                    folds.Add((fold[0], int.Parse(fold[1])));
                }
                currLine = Console.ReadLine();
            }

            printCoords();

            // Do the fold action.
            foreach ((string c, int n) in folds)
            {
                for (int i = 0; i < coords.Count; i++)
                {
                    (int x, int y) = coords[i];
                    if (c == "x" && x > n)
                    {
                        int newX = x - 2 * (x - n);
                        coords[i] = (newX, y);
                    }
                    if (c == "y" & y > n)
                    {
                        int newY = y - 2 * (y - n);
                        coords[i] = (x, newY);
                    }

                }

                // Remove duplicates.
                List<(int, int)> newCoords = coords.Distinct().ToList();
                coords = newCoords;
            }

            printCoords();
            Console.WriteLine("Result: " + coords.Count);
        }

        public static void printCoords()
        {
            int maxX = coords.Max(x => x.Item1) + 1;
            int maxY = coords.Max(y => y.Item2) + 1;
            bool[,] grid = new bool[maxX, maxY];

            foreach ((int x, int y) in coords)
            {
                grid[x, y] = true;
            }

            for (int x = maxX - 1; x >= 0; x--)
            {
                string currLine = "";
                for (int y = 0; y < maxY; y++)
                {
                    if (grid[x, y])
                        currLine += "#";
                    else
                        currLine += ".";
                }
                Console.WriteLine(currLine);
            }
        }
    }
}
