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

            Point[][] heightmap = new Point[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                Point[] numArray = Array.ConvertAll(lines[i].ToCharArray(), c => new Point((int)Char.GetNumericValue(c), false));
                heightmap[i] = numArray;
            }

            List<int> basinTotals = new List<int>();

            for (int x = 0; x < heightmap[0].Length; x++)
            { 
                for (int y = 0; y < heightmap.Length; y++)
                {
                    Point currNum = heightmap[y][x];
                    int basinTotal = checkBorders(heightmap, x, y, currNum);
                    basinTotals.Add(basinTotal);
                }
            }

            int[] total = basinTotals.OrderByDescending(n => n).Take(4).ToArray();

            // Do something with the input after runtime.
            Console.WriteLine("Result: " + total);
        }

        static int checkBorders(Point[][] heightMap, int x, int y, Point p)
        {
            int totalBasin = 0;
            int currNum = p.num;
            Point[][] newHeightMap = heightMap;
            newHeightMap[y][x].flooded = true;

            try {
                if (!heightMap[y-1][x].flooded && currNum + 1 == heightMap[y - 1][x].num) // Check up
                    totalBasin += 1 + checkBorders(newHeightMap, x, y - 1, newHeightMap[y - 1][x]);
            } catch { }
            try
            {
                if (!heightMap[y + 1][x].flooded && currNum + 1 == heightMap[y + 1][x].num) // Check check
                    totalBasin += 1 + checkBorders(newHeightMap, x, y + 1, newHeightMap[y + 1][x]);
            } catch { }
            try
            {
                if (!heightMap[y][x + 1].flooded && currNum + 1 == heightMap[y][x + 1].num) // Check right
                    totalBasin += 1 + checkBorders(newHeightMap, x + 1, y, newHeightMap[y][x + 1]);
            } catch { }
            try
            {
                if (!heightMap[y][x - 1].flooded && currNum + 1 == heightMap[y][x - 1].num) // Check down
                    totalBasin += 1 + checkBorders(newHeightMap, x - 1, y, newHeightMap[y][x - 1]);
            } catch { }

            return totalBasin;
        }
    }
    public class Point
    {
        public int num;
        public bool flooded;

        public Point(int _num, bool _flooded)
        {
            num = _num;
            flooded = _flooded;
        }
    }
}
