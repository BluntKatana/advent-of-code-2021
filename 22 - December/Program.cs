using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {

        //public static int boundary = 50;

        public static HashSet<(int, int, int)> cuboid = new HashSet<(int, int, int)>();

        static void Main(string[] args)
        {

            // Read the first line of input.
            string currLine = Console.ReadLine();

            // Check when all input is read.
            while (currLine != ":q")
            {
                string command = currLine.Split(" ")[0];
                var numbers = Regex.Matches(currLine, "-?[0-9]+");
                int[] xRange = { int.Parse(numbers[0].Value), int.Parse(numbers[1].Value) };
                int[] yRange = { int.Parse(numbers[2].Value), int.Parse(numbers[3].Value) };
                int[] zRange = { int.Parse(numbers[4].Value), int.Parse(numbers[5].Value) };

                if (command == "on")
                    turnOnRange(xRange, yRange, zRange);
                else
                    turnOffRange(xRange, yRange, zRange);

                currLine = Console.ReadLine();
            }

            // Do something with the input after runtime.
            Console.WriteLine("Result: " + cuboid.Count);
        }

        public static void turnOnRange(int[] xs, int[] ys, int[] zs)
        {
            for (int x = xs[0]; x <= xs[1]; x++)
            {
                for (int y = ys[0]; y <= ys[1]; y++)
                {
                    for (int z = zs[0]; z <= zs[1]; z++)
                    {
                        if (!cuboid.Contains((x, y, z)))
                            cuboid.Add((x, y, z));
                    }
                }
            }
        }

        public static void turnOffRange(int[] xs, int[] ys, int[] zs)
        {
                for (int x = xs[0]; x <= xs[1]; x++)
                {
                    for (int y = ys[0]; y <= ys[1]; y++)
                    {
                        for (int z = zs[0]; z <= zs[1]; z++)
                        {
                            if (cuboid.Contains((x, y, z)))
                                cuboid.Remove((x, y, z));
                        }
                    }
                }
        }
    }
}
