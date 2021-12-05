using System;

namespace _2___December
{
    class Program
    {
        static void Main(string[] args)
        {
            int hor = 0, aim= 0, ver = 0;

            string currLine = Console.ReadLine();

            // Check when all input is read.
            while (currLine != ":q")
            { 
                // Parse the input
                string[] keys = currLine.Split(" ");
                int keyNum = int.Parse(keys[1]);
                switch (keys[0])
                {
                    case "forward":
                        hor += keyNum;
                        ver += aim * keyNum;
                        break;
                    case "down":
                        aim += keyNum;
                        break;
                    case "up":
                        aim -= keyNum;
                        break;
                }

                // Read new line
                currLine = Console.ReadLine();
            }

            // Do something with the input after runtime.
            Console.WriteLine(hor * ver);
        }
    }
}
