using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class smth
    {
        static void Main(string[] args)
        {

            // Read the first line of input.
            string currLine = Console.ReadLine();
            Queue<int> slidingWindow = new Queue<int>();

            int depth = 0;

            // Check when all input is read.
            while (currLine != ":q")
            {
                // Parse the input -- currLine.
                int currNum = int.Parse(currLine);
                slidingWindow.Enqueue(currNum);

                if (slidingWindow.Count > 3)
                {
                    int prevNum = slidingWindow.Dequeue();
                    if (prevNum < currNum)
                        depth++;
                }

                // Read new line.
                currLine = Console.ReadLine();
            }

            // Print the result.
            Console.WriteLine("Result: " + depth);
        }
    }
}
