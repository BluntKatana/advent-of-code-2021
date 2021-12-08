using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        

        static void Main(string[] args)
        {
            // Initialize variables.

            // Part1(80)
            Part2(256);
        }
        
        public static void Part2(int days)
        {
            long[] lanterFish = new long[9];

            // Read the first line of input.
            string[] initialState = Console.ReadLine().Split(",");
            for (int i = 0; i < initialState.Length; i++)
            {
                int fish = int.Parse(initialState[i]);
                lanterFish[fish] += 1;
            }

            // Cycle through the days.
            for (int day = 1; day <= days; day++)
            {
                long[] lanterFishNew = new long[9];
                for (int i = lanterFish.Length - 1; i >= 0; i--)
                {
                    if (lanterFish[0] > 0 && i == 0)
                    {
                        lanterFishNew[8] += lanterFish[0];
                        lanterFishNew[6] += lanterFish[0];
                    }
                    else if (i != 0)
                    {
                        lanterFishNew[i - 1] = lanterFish[i];
                    }
                }
                lanterFish = lanterFishNew;

                //printFish(lanterFish);
            }

            // Print the final puzzle answer.
            long total = 0;
            for (int i = 0; i <= 8; i++)
                total += lanterFish[i];
            Console.WriteLine("Result: " + total);
        }

        public static void Part1(int days)
        {
            List<int> lanterFish = new List<int>();

            // Read the first line of input.
            string[] initialState = Console.ReadLine().Split(",");
            for (int i = 0; i < initialState.Length; i++)
                lanterFish.Add(int.Parse(initialState[i]));

            for (int day = 1; day <= days; day++)
            {
                for (int i = 0; i < lanterFish.Count; i++)
                {
                    // If a fish count timer
                    if (lanterFish[i] == 0)
                    {
                        lanterFish.Add(9);
                        lanterFish[i] = 6;
                    }
                    else
                        lanterFish[i] -= 1;
                }

                //printFish(lanterFish);
            }
            // Do something with the input after runtime.
            Console.WriteLine("Result: " + lanterFish.Count);
        }

        public static void printFish(int[] lanterFish)
        {
            Console.WriteLine(String.Join(", ", lanterFish));
        }
    }
}
