using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> input = new List<string>();
            string currLine = Console.ReadLine();
            while (currLine != ":q") {
                input.Add(currLine);
                currLine = Console.ReadLine();
            }

            string[] lines = input.ToArray();

            int[][] heightmap = new int[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                int[] numArray = Array.ConvertAll(lines[i].ToCharArray(), c => (int)Char.GetNumericValue(c));
                heightmap[i] = numArray;
            }

            int total = 0;
            for (int x = 0; x < heightmap[0].Length; x++)
            { 
                for (int y = 0; y < heightmap.Length; y++)
                {
                    int currNum = heightmap[y][x];
                    if (checkBorder(heightmap, x, y, currNum))
                        total += currNum + 1;
                }
            }

            // Do something with the input after runtime.
            Console.WriteLine("Result: " + total);
        }

        static public bool checkBorder(int[][] heightMap, int x, int y, int currNum)
        {
            if (x == 0 && y == 0) // Topleft
                return currNum < heightMap[y + 1][x] && currNum < heightMap[y][x + 1];
            else if (x == 0 && y == heightMap.Length - 1)  // Bottomleft
                return currNum < heightMap[y - 1][x] && currNum < heightMap[y][x + 1];
            else if (x == heightMap[0].Length - 1 && y == 0)  // Topright
                return currNum < heightMap[y + 1][x] && currNum < heightMap[y][x - 1];
            else if (x == heightMap[0].Length - 1 && y == heightMap.Length - 1) // Bottomright
                return currNum < heightMap[y - 1][x] && currNum < heightMap[y][x - 1];
            else if (x == 0) // At the left border
                return currNum < heightMap[y - 1][x] && currNum < heightMap[y + 1][x] && currNum < heightMap[y][x + 1];
            else if (x == heightMap[0].Length - 1) // At the right border
                return currNum < heightMap[y - 1][x] && currNum < heightMap[y + 1][x] && currNum < heightMap[y][x - 1];
            else if (y == 0) // At the top border
                return currNum < heightMap[y + 1][x] && currNum < heightMap[y][x - 1] && currNum < heightMap[y][x + 1];
            else if (y == heightMap.Length - 1) // At the bottom border
                return currNum < heightMap[y - 1][x] && currNum < heightMap[y][x - 1] && currNum < heightMap[y][x + 1];
            else // At no border!!
                return currNum < heightMap[y - 1][x] && currNum < heightMap[y + 1][x] && currNum < heightMap[y][x - 1] && currNum < heightMap[y][x + 1];
        }
    }
}
