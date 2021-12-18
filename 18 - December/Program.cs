using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {

            string currLine = Console.ReadLine();
            bool lastLine = false;
            while (!lastLine)
            {
                bool going = true;
                while (going)
                {
                    (string explodedCurrLine, bool explodedChange) = explode(currLine);
                    if (explodedChange)
                        currLine = explodedCurrLine;
                    else
                    {
                        (string splitCurrLine, bool splitChange) = split(currLine);
                        if (splitChange)
                            currLine = splitCurrLine;
                        else
                            going = false;
                    }
                }

                //Console.WriteLine(currLine);

                string newLine = Console.ReadLine();
                if (newLine == "") break;
                currLine = add(currLine, newLine);
            }


            Console.WriteLine("Result: " + currLine);
        }

        public static string add(string str1, string str2)
        {
            return "[" + str1 + "," + str2 + "]";
        }

        public static (string, bool) explode(string str)
        {
            int depth = 0;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c == '[') depth++;
                if (c == ']') depth--;

                if (depth >= 5 && char.IsDigit(str[i + 1]))
                {
                    string substring = str[i..(i+5)];
                    int leftVal      = substring[1] - '0';
                    int rightVal     = substring[3] - '0';

                    str = str.Remove(i, 5);

                    // Check for left value
                    var leftMatch = Regex.Match(str[..i], "[0-9]+", RegexOptions.RightToLeft);
                    bool leftBound = leftMatch.Success;

                    // Check for right value
                    var rightMatch = Regex.Match(str[i..], "[0-9]+");
                    bool rightBound = rightMatch.Success;

                    // Do some string manipulation
                    if (rightBound)
                    {
                        int newRightVal = rightVal + int.Parse(rightMatch.Value);
                        if (str[i..].First() == ',' && char.IsDigit(str[i + 1]))
                            str = str.Remove(i, rightMatch.Value.Length + 1).Insert(i, "0," + newRightVal.ToString());
                        else 
                            str = str.Remove(i + rightMatch.Index, rightMatch.Value.Length).Insert(i + rightMatch.Index, newRightVal.ToString()); 
                    }
                    if (leftBound)
                    {
                        int newLeftVal = leftVal + int.Parse(leftMatch.Value);
                        if (str[..i].Last() == ',' && char.IsDigit(str[i - 2]))
                            str = str.Remove(leftMatch.Index, leftMatch.Value.Length + 1).Insert(leftMatch.Index, newLeftVal.ToString() + ",0");
                        else
                            str = str.Remove(leftMatch.Index, leftMatch.Value.Length).Insert(leftMatch.Index, newLeftVal.ToString());
                    }
                    if (rightBound && leftBound)
                    {
                        if (str[i..(i + 2)] == "[]")
                            str = str.Remove(i, 3);
                        if (str[i..(i + 2)] == "[,")
                            str = str.Remove(i + 1, 1);
                        if (str[i] == ',')
                            str = str.Remove(i, 1);
                    }

                    return (str, true);
                }

            }

            return (str, false);
        }

        public static (string, bool) split(string str)
        {
            var match = Regex.Match(str, "[1-9][0-9]+");

            if (match.Success)
            {
                int val         = int.Parse(match.Value);
                int leftVal     = (int)Math.Floor((double)val / 2);
                int rightVal    = (int)Math.Ceiling((double)val / 2);
                string res = "[" + leftVal + "," + rightVal + "]";
                return (str.Replace(match.Value, res), true);
            }

            return (str, false);
        }
    }
}
