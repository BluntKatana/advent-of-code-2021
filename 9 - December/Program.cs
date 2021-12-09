using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public static bool[,] checkHeightMap;

        static void Main(string[] args)
        {
            List<string> input = new List<string>();
            string currLine = Console.ReadLine();
            while (currLine != ":q") {
                input.Add(currLine);
                currLine = Console.ReadLine();
            }

            string[] lines = input.ToArray();
            int x_length = lines.Length;
            int y_length = lines[0].Length;

            int[,] heightmap = new int[x_length,y_length];

            for (int i = 0; i < x_length; i++)
                for (int j = 0; j < y_length; j++)
                    heightmap[i, j] = (int)Char.GetNumericValue(lines[i][j]);
         
            List<int> basinTotals = new List<int>();

            List<(int, int)> lowPoints = getLowPoints(heightmap, x_length, y_length);

            foreach((int x, int y) in lowPoints)
            {
                checkHeightMap = new bool[x_length, y_length];
                basinTotals.Add(1 + checkBorders(heightmap, x, y));
            }

            int[] totalArr = basinTotals.OrderByDescending(n => n).Take(3).ToArray();
            int total = 1;
            foreach (int i in totalArr)
                total *= i;

            // Do something with the input after runtime.
            Console.WriteLine("Result: " + total);
        }

        static int checkBorders(int[,] heightMap, int x, int y)
        {
            int totalBasin = 0;
            int currNum = heightMap[x, y];

            checkHeightMap[x, y] = true;

            try {
                if (!checkHeightMap[x, y-1] && currNum < heightMap[x, y-1] && heightMap[x, y-1] != 9) // Check up
                    totalBasin += 1 + checkBorders(heightMap, x, y - 1);
            } catch { }
            try
            {
                if (!checkHeightMap[x, y+1] && currNum < heightMap[x, y+1] && heightMap[x, y+1] != 9) // Check down
                    totalBasin += 1 + checkBorders(heightMap, x, y + 1);
            } catch { }
            try
            {
                if (!checkHeightMap[x + 1, y] && currNum < heightMap[x + 1, y] && heightMap[x+1, y] != 9) // Check right
                    totalBasin += 1 + checkBorders(heightMap, x + 1, y);
            } catch { }
            try
            {
                if (!checkHeightMap[x - 1, y] && currNum < heightMap[x-1, y] && heightMap[x-1, y] != 9) // Check left
                    totalBasin += 1 + checkBorders(heightMap, x - 1, y);
            } catch { }

            return totalBasin;
        }

        static List<(int, int)> getLowPoints(int[,] heightMap, int x_length, int y_length)
        {
            List<(int, int)> lowPoints = new List<(int, int)>();

            for (int x = 0; x < x_length; x++)
            {
                for (int y = 0; y < y_length; y++)
                {
                    int currNum = heightMap[x, y];
                    int check = 0;
                    try
                    {
                        if (currNum < heightMap[x, y - 1]) // Check up
                            check += 1;
                    }
                    catch { check += 1; }
                    try
                    {
                        if (currNum < heightMap[x, y + 1]) // Check down
                            check += 1;
                    }
                    catch { check += 1; }
                    try
                    {
                        if (currNum < heightMap[x + 1, y])// Check right
                            check += 1;
                    }
                    catch { check += 1; }
                    try
                    {
                        if (currNum < heightMap[x - 1, y]) // Check left
                            check += 1;
                    }
                    catch { check += 1; }
                    if (check == 4)
                        lowPoints.Add((x, y));
                }
            }

            return lowPoints;
        }
    }

}
