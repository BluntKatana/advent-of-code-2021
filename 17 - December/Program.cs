using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        public static int x_low, x_high;
        public static int y_low, y_high;
        static void Main(string[] args)
        {

            // -- | Parse the input using Regex
            string currLine = Console.ReadLine();
            var numbers = Regex.Matches(currLine, "-?[0-9]+");
            
            x_low   = int.Parse(numbers[0].Value);
            x_high  = int.Parse(numbers[1].Value);
            y_low   = int.Parse(numbers[2].Value);
            y_high  = int.Parse(numbers[3].Value);

            // -- | Initialize boundaries
            // x_velocity cannot be negative nor more than x_high
            int x_vel_min = 0; 
            int x_vel_max = x_high;  

            // brute force this, seemed to work lmfao
            int y_vel_min = y_low * 2; 
            int y_vel_max = -y_low * 2;

            // -- | Part 1
            int y_max = y_low;

            // -- | Part 2
            int reached = 0;
            List<(int, int)> velocityValues = new List<(int, int)>();

            // -- | Double loop through all possible velocity values and try to catch the target
            for (int x_vel = x_vel_min; x_vel <= x_vel_max; x_vel++)
            {
                for (int y_vel = y_vel_min; y_vel <= y_vel_max; y_vel++)
                {
                    int x = 0, y = 0, temp_y_max = y_low;
                    int tx_vel = x_vel, ty_vel = y_vel;
                    while (x <= x_high && y >= y_low)
                    {
                        x += tx_vel;
                        y += ty_vel;

                        if (tx_vel > 0) tx_vel--;
                        if (tx_vel < 0) tx_vel++;

                        ty_vel--;

                        temp_y_max = Math.Max(temp_y_max, y);

                        if (x >= x_low && x <= x_high && y >= y_low && y <= y_high)
                        {
                            if (!velocityValues.Contains((x_vel, y_vel))) {
                                velocityValues.Add((x_vel, y_vel));
                                reached++;
                            }
                            y_max = Math.Max(y_max, temp_y_max);
                        }
                    }
                }
            }

            Console.WriteLine("Result: " + reached);
        }
    }
}
