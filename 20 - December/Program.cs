using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Program
    {
        // Example was size 5x5
        // Input   was size 100x100
        public static int size = 100;
        public static int offset = 1;
        public static string algorithm = "";
        static void Main(string[] args)
        {
            algorithm = Console.ReadLine();
            algorithm = '.' + algorithm.Remove(0, 1);
            _ = Console.ReadLine();
            string currLine = Console.ReadLine();
            List<string> lines = new List<string>();
            while (currLine != "")
            {
                lines.Add(currLine);
                currLine = Console.ReadLine();
            }

            size = lines[0].Length;

            // Creating starting image
            bool[,] originalImage = new bool[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (lines[i][j] == '#') originalImage[i, j] = true;

            bool[,] enhanced1xImage = enhanceImage(originalImage, 0);
            printImage(enhanced1xImage, 0);

            bool[,] enhanced2xImage = enhanceImage(enhanced1xImage, 0);
            printImage(enhanced2xImage, 1);

            Console.WriteLine(countTotal(enhanced2xImage));
        }

        public static int countTotal(bool[,] image)
        {
            int total = 0;
            for (int i = 0; i < image.GetLength(0); i++)
                for (int j = 0; j < image.GetLength(1); j++)
                    if (image[i, j]) total++;

            return total;
        }

        public static bool[,] enhanceImage(bool[,] image, int iter)
        {
            int y_size = image.GetLength(0);
            int x_size = image.GetLength(1);

            bool[,] newImage = new bool[y_size + 2 * offset, x_size + 2 * offset];

            for (int y = -offset; y < y_size + offset; y++)
            {
                for (int x = -offset; x < x_size + offset; x++)
                {
                    string binary = "";
                    foreach ((int dx, int dy) in getCoords())
                    {
                        // Boundary detection
                        if (iter % 2 == 0)
                        { // if we are odd, infinite lit, even infinite off.
                            if (y + dy >= 0 && x + dx >= 0 && y + dy < y_size && x + dx < x_size)
                            {
                                if (image[y + dy, x + dx])
                                    binary += '1';
                                else
                                    binary += '0';
                            }
                            else
                            {
                                binary += '0';
                            }
                        }
                        else
                        {
                            if (y + dy >= 2 && x + dx >= 2 && y + dy < y_size && x + dx < x_size)
                            {
                                if (image[y + dy, x + dx])
                                    binary += '1';
                                else
                                    binary += '0';
                            }
                            else
                                binary += '1';
                        }
                    }
                    int number = Convert.ToInt32(binary, 2);
                    newImage[y + offset, x + offset] = algorithm[number] == '#' ? true : false;
                }
            }

            return newImage;

            List<(int, int)> getCoords()
            {
                return new List<(int, int)>() {
                    (-1,-1), (0, -1), (1, -1),
                    (-1, 0), (0,  0), (1,  0),
                    (-1, 1), (0,  1), (1,  1)
                };
            }
        }

        public static void printImage(bool[,] image, int iter)
        {
            int y_max = image.GetLength(0);
            int x_max = image.GetLength(1);
            Console.WriteLine("--- " + iter);
            for (int y = 0; y < y_max; y++)
            {
                string curr = "";
                for (int x = 0; x < x_max; x++)
                {
                    if (image[y, x])
                        curr += "#";
                    else
                        curr += ".";
                }
                Console.WriteLine(curr);
                
            }
        }
    }
}
