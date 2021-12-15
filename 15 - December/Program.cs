using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        public static int size = 10;

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
            Console.WriteLine("Result: " + shortestPath(grid, src, dest));
        }



        // Function to find the length of the
        // shortest path with neighbor nodes
        // value not exceeding K
        public static int shortestPath(int[,] mat, int[] src, int[] dest)
        {
            // Initialize a queue
            Queue<Node> q = new Queue<Node>();

            // Add the source node
            // into the queue
            q.Enqueue(new Node(src[0], src[1], 0, mat[src[0], src[1]]));

            // Initialize rows and cols
            int N = mat.GetLength(0), M = mat.GetLength(1);

            // Initialize a bool matrix
            // to keep track of visisted cells
            bool[,] visited = new bool[N, M];

            // Initialize the directions
            int[,] dir = { { -1, 0 }, { 1, 0 },
                        { 0, 1 }, { 0, -1 } };

            // Apply BFS
            while (q.Count != 0)
            {

                Node curr = q.Peek();
                q.Dequeue();

                // If cell is already visited
                if (visited[curr.i, curr.j])
                    continue;

                // Mark current node as visited
                visited[curr.i, curr.j] = true;

                // Return the answer after
                // reaching the destination node
                if (curr.i == dest[0] && curr.j == dest[1])
                    return curr.dist;

                // Explore neighbors
                for (int i = 0; i < 4; i++)
                {

                    int x = dir[i, 0] + curr.i, y = dir[i, 1] + curr.j;

                    // If out of bounds or already visited
                    if (x < 0 || y < 0 || x == N || y == M || visited[x, y])
                        continue;

                    // Add current cell into the queue
                    q.Enqueue(new Node(x, y, curr.dist + 1, mat[x, y]));
                }
            }

            // No path exists return -1
            return -1;
        }
    }

    class Node
    {

        public int dist, i, j, val;

        // Constructor
        public Node(int i, int j,
                    int dist, int val)
        {
            this.i = i;
            this.j = j;
            this.dist = dist;
            this.val = val;
        }
    }
}
