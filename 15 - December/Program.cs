using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public static int size;
        public static int duplicate = 5;

        static void Main(string[] args)
        {
            // Read input
            List<string> lines = new List<string>();
            string currLine = Console.ReadLine();
            while (currLine != ":q")
            {
                lines.Add(currLine);
                currLine = Console.ReadLine();
            }

            size = lines.Count();

            // Parse input
            int[,] originalGrid = new int[size,size];
            for (int i = 0; i < lines.Count(); i++)
                for (int j = 0; j < lines[i].Count(); j++)
                    originalGrid[i,j] = int.Parse(lines[i][j].ToString());

            int[,] biggerGrid = createBiggerGrid(originalGrid, duplicate);

            // Do something with the input after runtime.
            Console.WriteLine("Result: " + shortestPath(biggerGrid, size * duplicate));
        }

        public static int[,] createBiggerGrid(int[,] grid, int amount)
        {
            int[,] biggerGrid = new int[size * amount, size * amount];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++) 
                    for (int hor = 0; hor < amount; hor++)
                        for (int ver = 0; ver < amount; ver++)
                            biggerGrid[i + size * hor, j + size * ver] = wrapAround(grid[i, j] + hor + ver);

            return biggerGrid;
        }

        public static int wrapAround(int i)
        {
            return (i % 9 == 0) ? 9 : i % 9;
        }

        public static bool isValid(int i, int j, int amount)
        {
            return i >= 0 && i < amount && j >= 0 && j < amount;
        }

        public static int shortestPath(int[,] grid, int amount)
        {
            // Initializing distance array.
            int[,] distance = new int[amount, amount];
            for (int i = 0; i < amount; i++)
                for (int j = 0; j < amount; j++)
                    distance[i, j] = int.MaxValue;

            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };

            // Initialize first node.
            List<Node> set = new List<Node>();
            set.Add(new Node(0, 0, 0));
            distance[0, 0] = 0;
             
            while (distance[amount - 1, amount - 1] == int.MaxValue)
            {
                // Get the node with minimum distance and delete it.
                set.Sort((Node a, Node b) => a.dist.CompareTo(b.dist));
                int min = set.Min(n => n.dist);
                Node k = set.Find(n => n.dist == min);
                set.Remove(k);

                for (int i = 0; i < 4; i++)
                {
                    int x = k.x + dx[i];
                    int y = k.y + dy[i];

                    if (!isValid(x, y, amount))
                        continue;

                    if (distance[x, y] > distance[k.x, k.y] + grid[x, y])
                    {
                        if (distance[x, y] != int.MaxValue)
                            set.Remove(set.Find(n => n == new Node(x, y, distance[x, y])));

                        distance[x, y] = distance[k.x, k.y] + grid[x, y];
                        set.Add(new Node(x, y, distance[x, y]));
                    }
                }
            }

            return distance[amount - 1, amount - 1];

        }
    }

    class Node
    {
        public int dist, x, y;
        public Node(int x, int y,
                    int dist)
        {
            this.x = x;
            this.y = y;
            this.dist = dist;
        }
    }
}
