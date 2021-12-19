using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Program
    {
        public static List<Blob> snailfish = new List<Blob>();
        static void Main(string[] args)
        {
            string currLine = Console.ReadLine();
            List<string> lines = new List<string>();
            while (currLine != "")
            {
                lines.Add(currLine);
                currLine = Console.ReadLine();
                
            }

            long maxMag = 0;
            // Recurse over every possible
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    // x + y
                    snailfish.Clear();
                    add(parseToBlobs(lines[i]), parseToBlobs(lines[j]));
                    reduce();
                    maxMag = Math.Max(maxMag, magnitude(snailfish));

                    // y + x
                    snailfish.Clear();
                    add(parseToBlobs(lines[j]), parseToBlobs(lines[i]));
                    reduce();
                    maxMag = Math.Max(maxMag, magnitude(snailfish));
                }
            }
            Console.WriteLine(maxMag);
        }

        public static void reduce()
        {
            while (explode() || split()) {; }
        }

        public static long magnitude(List<Blob> snailList)
        {
            // remove beginning and end brackets
            if (!(snailList[0].open || snailList[0].close)) {
                return snailList[0].value;
            }

            snailList.RemoveAt(snailList.Count - 1); snailList.RemoveAt(0);

            int open = 0, close = 0;

            for (int i = 0; i < snailList.Count; i++)
            {
                if (snailList[i].open) open++;
                if (snailList[i].close) close++;
                if (open == close)
                {
                    List<Blob> leftSide = snailList.GetRange(0, i + 1);
                    List<Blob> rightSide = snailList.GetRange(i + 1, snailList.Count - i - 1);

                    return 3 * magnitude(leftSide) + 2 * magnitude(rightSide);
                }
                    
            }

            return 1;
        }


        public static int howManyNums(List<Blob> snailList)
        {
            return snailList.FindAll(v => v.value >= 0).Count;
        }
        public static void add(List<Blob> one, List<Blob> two)
        {
            snailfish.AddRange(one);
            snailfish.AddRange(two);
            snailfish.Insert(0, new Blob(-1, true, false)); // Add open bracket
            snailfish.Add(new Blob(-1, false, true));       // Add closing bracket
        }

        public static bool split()
        {
            for (int i = 0; i < snailfish.Count; i++)
            {
                if (snailfish[i].value > 9)
                {
                    int val = snailfish[i].value;
                    int leftVal = (int)Math.Floor((double)val / 2);
                    int rightVal = (int)Math.Ceiling((double)val / 2);

                    // Replace high value with pair of low values
                    snailfish.RemoveAt(i);
                    List<Blob> newPair = new List<Blob>() {
                        new Blob(-1, true, false),
                        new Blob(leftVal, false, false),
                        new Blob(rightVal, false, false),
                        new Blob(-1, false, true)
                    };
                    snailfish.InsertRange(i, newPair);

                    return true;
                }
            }

            return false;
        }

        public static bool explode()
        {
            int depth = 0;
            for (int i = 0; i < snailfish.Count; i++)
            {
                if (snailfish[i].open)
                {
                    depth++;
                    if (depth == 5)
                    {
                        // Find left value
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (snailfish[j].value >= 0)
                            {
                                snailfish[j].value = snailfish[j].value + snailfish[i + 1].value;
                                break;
                            }
                        }

                        // Find right value
                        for (int j = i + 3; j < snailfish.Count; j++)
                        {
                            if (snailfish[j].value >= 0)
                            {
                                snailfish[j].value = snailfish[j].value + snailfish[i + 2].value;
                                break;
                            }
                        }

                        // Replace pair with 0
                        snailfish.RemoveRange(i, 4);
                        snailfish.Insert(i, new Blob(0, false, false));

                        return true;
                    }
                } 
                else if (snailfish[i].close)
                {
                    depth--;
                }
            }

            return false;
        }

        public static List<Blob> parseToBlobs(string input)
        {
            List<Blob> tempSnailfish = new List<Blob>();

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                switch (c)
                {
                    case '[':
                        tempSnailfish.Add(new Blob(-1, true, false));
                        break;
                    case ']':
                        tempSnailfish.Add(new Blob(-1, false, true));
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
                        tempSnailfish.Add(new Blob(int.Parse(val), false, false));
                        break;
                }
            }
            return tempSnailfish;
        }

        public static string printBlobList(List<Blob> str)
        {
            string res = "";
            foreach (Blob b in str)
            {
                if (b.open) res += "[";
                if (b.close) res += "]";
                if (b.value >= 0) res += b.value;
            }

            return res;
        }
    }

    class Blob
    {
        public int value;
        public bool open, close;
        public Blob(int value, bool open, bool close)
        {
            this.value = value;
            this.open = open;
            this.close = close;
        }
    }
}
