using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string imageEnhancementAlgorithm = Console.ReadLine();
            _ = Console.ReadLine();
            string currLine = Console.ReadLine();
            List<string> lines = new List<string>();
            while (currLine != "") { 
                lines.Add(currLine);
                currLine = Console.ReadLine();
            }

            // Create starting image
            Dictionary<(int, int), char) startingImage = new char[lines.Count, lines[0].Length];
            for (int x = 0; x < lines.Count; x++)
                for (int y = 0; y < lines[0].Length; y++)
                    startingImage[x, y] = lines[x][y];

            // Enhance 1 time
            char[,] newImage = enhance(startingImage, imageEnhancementAlgorithm);
            //printImage(newImage);

            // Enhance second time
            char[,] ultraEnhancedImage = enhance(newImage, imageEnhancementAlgorithm);
            //printImage(ultraEnhancedImage);

            int total = 0;

            for (int i = 0; i < ultraEnhancedImage.GetLength(1); i++)
                for (int j = 0; j < ultraEnhancedImage.GetLength(0); j++)
                    if (ultraEnhancedImage[i, j] == '#') total++;


            // Do something with the input after runtime.
            Console.WriteLine("Result: " + total);
        }

        static public char[,] enhance(char[,] image, string algorithm)
        {
            int new_y_size = 3 * image.GetLength(1);
            int new_x_size = 3 * image.GetLength(0);
            char[,] tempNewImage = new char[new_x_size, new_y_size];
            char[,] newImage = new char[new_x_size, new_y_size];
            
            // Project oldImage onto tempNewImage
            for (int y = 0; y < image.GetLength(1); y++)
            {
                for (int x = 0; x < image.GetLength(0); x++)
                {
                    tempNewImage[x + image.GetLength(0), y + image.GetLength(1)] = image[x, y];
                }
            }


            // Get the new pixel values
            for (int y = 0; y < new_y_size; y++)
            {
                for (int x = 0; x < new_x_size; x++)
                {
                    string binaryString = "";
                    try { binaryString += toBinary(tempNewImage[y-1, x-1    ]); } catch { binaryString += "0"; }   // Top left
                    try { binaryString += toBinary(tempNewImage[y-1, x      ]); } catch { binaryString += "0"; }   // Top middle
                    try { binaryString += toBinary(tempNewImage[y-1, x+1    ]); } catch { binaryString += "0"; }   // Top right
                    try { binaryString += toBinary(tempNewImage[y  , x-1    ]); } catch { binaryString += "0"; }   // Middle left
                    try { binaryString += toBinary(tempNewImage[y  , x      ]); } catch { binaryString += "0"; }   // Middle middle
                    try { binaryString += toBinary(tempNewImage[y  , x+1    ]); } catch { binaryString += "0"; }   // Middle right
                    try { binaryString += toBinary(tempNewImage[y+1, x-1    ]); } catch { binaryString += "0"; }   // Bottom left
                    try { binaryString += toBinary(tempNewImage[y+1, x      ]); } catch { binaryString += "0"; }   // Bottom middle
                    try { binaryString += toBinary(tempNewImage[y+1, x+1    ]); } catch { binaryString += "0"; }   // Bottom right
                    newImage[y, x] = algorithm[Convert.ToInt32(binaryString, 2)];
                }
            }

            return newImage;
        }

        static public string toBinary(char c)
        {
            if (c == '#')
                return "1";
            else
                return "0";
        }

        static public void printImage(char[,] grid)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                string currLine = "";
                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    if (grid[x, y] == '#')
                        currLine += grid[x, y];
                    else
                        currLine += '.';
                }
                Console.WriteLine(currLine);
            }

            Console.WriteLine("--------------------");
        }
    }
}
