using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string currLine = Console.ReadLine();
            int[] input = currLine.Split(",").Select(v => int.Parse(v)).ToArray();             
                                                                 
            int leastFuelCost = int.MaxValue;
            for (int i = input.Min(); i <= input.Max(); i++)
            {
                int totalCostI = 0;
                foreach (int j in input)
                    totalCostI += facultyWithPlus(Math.Abs(i - j));
                if (leastFuelCost > totalCostI) leastFuelCost = totalCostI;
            }
            
            Console.WriteLine("Result: " + leastFuelCost);          
        }                                   
       
                                                                    
        public static int facultyWithPlus(int temp)                         
        {
            int res = 0;
            for (int i = temp; i > 0; i--)
                res += i;
            return res;
        }
    }
}
