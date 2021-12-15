using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public static int size = 100;

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

            // Parse input
            int[,] grid = new int[size,size];
            for (int i = 0; i < lines.Count(); i++)
                for (int j = 0; j < lines[i].Count(); j++)
                    grid[i,j] = int.Parse(lines[i][j].ToString());

            int[] src = { 0, 0 };
            int[] dest = { size - 1, size - 1 };


            // Do something with the input after runtime.
            Console.WriteLine("Result: " + shortestPath(grid, size));
        }

        public static bool isValid(int i, int j)
        {
            return i >= 0 && i < size && j >= 0 && j < size;
        }

        public static int shortestPath(int[,] grid, int size)
        {
            // Initializing distance array.
            int[,] distance = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    distance[i, j] = int.MaxValue;

            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };

            // Initialize first node.
            List<Node> set = new List<Node>();
            set.Add(new Node(0, 0, 0));
            distance[0, 0] = 0;

            while (distance[size - 1, size - 1] == int.MaxValue)
            {
                // Get the node with minimum distance and delete it.
                int min = set.Min(n => n.dist);
                Node k = set.Find(n => n.dist == min);
                set.Remove(k);

                for (int i = 0; i < 4; i++)
                {
                    int x = k.x + dx[i];
                    int y = k.y + dy[i];

                    if (!isValid(x, y))
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

            return distance[size - 1, size - 1];

        }
    }

    class Node
    {

        public int dist, x, y;

        // Constructor
        public Node(int x, int y,
                    int dist)
        {
            this.x = x;
            this.y = y;
            this.dist = dist;
        }
    }
}
