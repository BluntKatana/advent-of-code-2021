using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        

        // Only works for square grids.
        public static int size = 10;
        public static int totalCells = size * size;
        public static int maxSteps = 100;
        public static int[,] grid;
        public static List<(int, int)> flashed = new List<(int, int)>();

        static void Main(string[] args)
        {
            grid = new int[size, size];
            
            // Parse input.
            for (int i = 0; i < size; i++)
            {
                int[] currLine = Array.ConvertAll(Console.ReadLine().ToCharArray(), c => (int)Char.GetNumericValue(c));
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = currLine[j];
                }
            }

            int step = 0;
            // Loop through steps.
            while (true) {
                // Next step.
                step++;
                printGrid(step);
                flashed.Clear();

                // Add 1 to all octopuses.
                for (int i = 0; i < totalCells; i++)
                        grid[i % size, i / size] += 1;

                // Check octupes larger 9 with flash potential.
                for (int i = 0; i < totalCells; i++)
                {
                    int x = i % size; int y = i / size;
                    if (grid[x, y] > 9 && !flashed.Contains((x, y)))
                    {
                        flashed.Add((x, y));
                        addNeighbours(x, y);
                    }
                }


                // Remove flash potential and reset to 0.
                foreach ((int x, int y) in flashed)
                    grid[x, y] = 0;

                // Check if all octupuses flashed.
                if (flashed.Count - 1 == 100)
                {
                    printGrid(step);
                    Console.WriteLine(step);
                    return;
                }

                // Print out the steps.
                Console.WriteLine(step);
            }
        }

        static public void addNeighbours(int i, int j)
        {
            flashed.Add((i, j));
            int[] dirs = new int[3] { -1, 1, 0 };

            foreach (int x in dirs)
            {
                foreach (int y in dirs)
                {
                    if (x == 0 && y == 0) break;
                    int newX = i + x; int newY = j + y;

                    try
                    {
                        grid[newX, newY] += 1;
                        if (grid[newX, newY] > 9 && !flashed.Contains((newX, newY)))
                            addNeighbours(newX, newY);
                    } catch { }
                }
            }
        }

        static public void printGrid(int step)
        {
            Console.WriteLine("After step: " + step);
            for (int i = 0; i < size; i++)
            {
                string currLine = "";
                for (int j = 0; j < size; j++)
                {
                    if (grid[i, j] == 0)
                        currLine += ".";
                    else
                        currLine += grid[i, j];
                }
                Console.WriteLine(currLine);
            }
        }
    }
}
