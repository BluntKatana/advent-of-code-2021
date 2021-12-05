 using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;

namespace Collatz
{
    class Program
    {
        public static int width = 10;
        public static int height = 10;

        static void Main(string[] args)
        {
            int[,] grid = new int[width, height];

            // Parse the input
            string currLine = Console.ReadLine();
            while (currLine != ":q")
            {
                // Do smth with the line
                string[] parts = currLine.Split(" ");
                string[] leftCoord_s = parts[0].Split(",");
                string[] rightCoord_s = parts[2].Split(",");

                int[] leftCoord = new int[2] { int.Parse(leftCoord_s[0]), int.Parse(leftCoord_s[1]) };   // [x1, y1]
                int[] rightCoord = new int[2] { int.Parse(rightCoord_s[0]), int.Parse(rightCoord_s[1]) };  // [x2, y2]

                int minY = Math.Min(leftCoord[1], rightCoord[1]);
                int maxY = Math.Max(leftCoord[1], rightCoord[1]);

                int minX = Math.Min(leftCoord[0], rightCoord[0]);
                int maxX = Math.Max(leftCoord[0], rightCoord[0]);

                // x-axis are the same
                if (leftCoord[0] == rightCoord[0])
                {
                    for (int i = minY; i <= maxY; i++)
                        grid[leftCoord[0], i] += 1;
                }
                // y-axis are the same 
                else if (leftCoord[1] == rightCoord[1])
                {
                    for (int i = minX; i <= maxX; i++)
                        grid[i, leftCoord[1]] += 1;
                } else
                // x-axis and y-axis are different
                {
                    //                   x_l            x_r                y_l          y_r       
                    bool downwards = leftCoord[0] < rightCoord[0] && leftCoord[1] < rightCoord[1] ||
                                     leftCoord[0] > rightCoord[0] && leftCoord[1] > rightCoord[1];
                    if (downwards)
                    {
                        int a = 0;
                        for (int x = minX; x <= maxX; x++)
                        {
                            int y = minY + a;
                            grid[x, y] += 1;
                            a++;
                        }
                    }
                    else
                    {
                        int a = 0;
                        for (int x = minX; x <= maxX; x++)
                        {
                            int y = maxY - a;
                            grid[x, y] += 1;
                            a++;
                        }
                    }
                }
                // Checks for new line
                currLine = Console.ReadLine();
            }

            // Do something with the input after all the parsing
            int points = 0;
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    if (grid[i, j] >= 2)
                        points++;

            printGrid(grid);
            Console.WriteLine("Result " + points);
        }

        public static void printGrid(int[,] grid)
        {
            for (int y = 0; y < grid.Length / height; y++)
            {
                string currLine = "";
                for (int x = 0; x < grid.Length / width; x++)
                {
                    if (grid[x, y] == 0)
                        currLine += ".";
                    else
                        currLine += grid[x, y];
                }
                Console.WriteLine(currLine);
            }
        }

    }
}
