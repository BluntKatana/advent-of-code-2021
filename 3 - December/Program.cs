using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace Collatz
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> nums = new List<string>();

            while (true)
            {
                // Gather all the ones and zeros.
                string newNum = Console.ReadLine();
                if (newNum != "") nums.Add(newNum);

                // If the last feels is empty.
                if (newNum == "")
                {
                    int oxygenNum   = filterToOneByte(nums, '1', '0');
                    int CO2Num      = filterToOneByte(nums, '0', '1');

                    // Prints the results.
                    Console.WriteLine("result: " + oxygenNum * CO2Num);
                    return;
                }
            }
        }

        static int filterToOneByte(List<string> nums, char oneBit, char zeroBit)
        {
            // Converts a bit string to an int.
            List<string> resList = nums;
            for (int i = 0; resList.Count > 1; i++)
            {
                if (moreOnes(resList, i))
                    resList = filterOnIndex(resList, i, oneBit);
                else
                    resList = filterOnIndex(resList, i, zeroBit);
            }

            return Convert.ToInt32(resList[0], 2);
        }

        // Filters a list of strings on index i with character bit.
        static List<string> filterOnIndex(List<string> nums, int i, char bit)
        {
            List<string> res = new List<string>();

            foreach (string num in nums)
                if (num[i] == bit)
                    res.Add(num);
            return res;
        }

        // Checks if there are more 1's at index i in a list of strings.
        static bool moreOnes(List<string> nums, int i)
        {
            int zeros = 0;
            int ones = 0;

            foreach (string num in nums)
            {
                if (num[i] == '1')
                    ones++;
                else
                    zeros++;
            }

            return (ones >= zeros);
        }
    }

}

/* 
00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010
     */
