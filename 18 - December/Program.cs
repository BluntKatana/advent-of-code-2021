using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Program
    {
        public static List<Blob> snailfish = new List<Blob>();
        public static bool explChange = true, splitChange = true;

        static void Main(string[] args)
        {
            string currLine = Console.ReadLine();
            snailfish = parseToBlobs(currLine, false);

            while (true)
            {              
                while (explChange || splitChange)
                {
                    explode();
                    if (!explChange) split();
                }
                currLine = Console.ReadLine();
                if (currLine == "") break;
                add(currLine);
                explChange = true; splitChange = true;
            }
        }

        public static void add(string addon)
        {
            for (int i = 0; i < snailfish.Count; i++)
            {
                snailfish[i].depth++;
                snailfish[i].open++;
            }

            snailfish.AddRange(parseToBlobs(addon, true));
        }

        public static void split()
        {
            for (int i = 0; i < snailfish.Count; i++)
            {
                Blob currBlob = snailfish[i];
                if (currBlob.value > 9)
                {
                    int newLeftVal  = (int)Math.Floor((double)currBlob.value / 2);
                    int newRightVal = (int)Math.Ceiling((double)currBlob.value / 2);

                    snailfish.Insert(i + 1, new Blob(newRightVal, currBlob.depth + 1, currBlob.open + 1, currBlob.close));
                    snailfish.Insert(i + 1, new Blob(newLeftVal, currBlob.depth + 1, currBlob.open + 1, currBlob.close));
                    snailfish.RemoveAt(i);
                    splitChange = true;

                    // Update the open and close values
                    for (int j = i + 2; j < snailfish.Count; j++)
                    {
                        snailfish[j].close++;
                        snailfish[j].open++;
                    }

                    return;
                }
            }

            splitChange = false;
        }

        public static void explode()
        {
            for (int i = 0; i < snailfish.Count; i++)
            {
                // If depth 5 is reached.
                if (snailfish[i].depth > 4)
                {
                    int leftVal = snailfish[i].value;
                    int rightVal = snailfish[i + 1].value;

                    explChange = true;

                    // No numbers more numbers to the left
                    if (i == 0)
                    {
                        snailfish[i + 2].value += rightVal;
                        snailfish.Insert(i + 2, new Blob(0, snailfish[i + 2].depth, snailfish[i + 2].open, snailfish[i + 2].close));
                        snailfish.RemoveAt(i); snailfish.RemoveAt(i);
                        return;
                    }

                    // No more numbers to the right
                    if (i == snailfish.Count - 2)
                    {
                        snailfish[i - 1].value += leftVal;
                        snailfish.Insert(i + 2, new Blob(0, snailfish[i - 1].depth, snailfish[i - 1].open, snailfish[i - 1].close));
                        snailfish.RemoveAt(i); snailfish.RemoveAt(i);
                        return;
                    }

                    // Numbers on the left and right
                    snailfish[i - 1].value += leftVal;
                    snailfish[i + 2].value += rightVal;
                    
                    // Check if the open-brackets from the #-left are 1 less than the current #
                    if (snailfish[i - 1].depth == snailfish[i].depth - 1 && snailfish[i-1].open + 1 == snailfish[i].open)
                        snailfish.Insert(i + 2, 
                            new Blob(0, snailfish[i - 1].depth, snailfish[i - 1].open, snailfish[i - 1].close));
                    
                    // Check if the close-brackets from the #-right are 1 more than the curent #
                    if (snailfish[i + 2].depth == snailfish[i].depth - 1 && snailfish[i].close - 1 == snailfish[i + 2].close)
                        snailfish.Insert(i + 2,
                            new Blob(0, snailfish[i + 2].depth, snailfish[i + 2].open, snailfish[i + 2].close));

                    snailfish.RemoveAt(i); snailfish.RemoveAt(i);

                    // Update the open and close values
                    for (int j = i; j < snailfish.Count; j++)
                    {
                        snailfish[j].close--;
                        snailfish[j].open--;
                    }

                    return;
                }
            }

            explChange = false;
        }

        public static List<Blob> parseToBlobs(string input, bool add)
        {
            List<Blob> tempSnailfish = new List<Blob>();
            int depth = 0, open = 0, close = 0;

            if (add)
            {
                Blob lastBlob = snailfish[snailfish.Count - 1];
                depth = 1; 
                open = lastBlob.open; 
                close = lastBlob.close + lastBlob.depth;
            }


            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                switch (c)
                {
                    case '[':
                        depth++;
                        open++;
                        break;
                    case ']':
                        depth--;
                        close++;
                        break;
                    case ',':
                        break;
                    default:
                        int nextchar = i;
                        string val = c.ToString();
                        while (char.IsDigit(input[nextchar + 1]))
                        {
                            val += input[nextchar + 1];
                            nextchar++;
                        }
                        i = nextchar;
                        tempSnailfish.Add(new Blob(int.Parse(val), depth, open, close));
                        break;
                }
            }
            return tempSnailfish;
        }
    }

    class Blob
    {
        public int value, depth, open, close;
        public Blob(int value, int depth, int open, int close)
        {
            this.value = value;
            this.depth = depth;
            this.open = open;
            this.close = close;
        }
    }
}
