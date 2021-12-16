using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Program
    {
        public static Queue<string> queue;
        static void Main(string[] args)
        {

            string hexaPackets = Console.ReadLine();
            string binaryPackets = String.Join(String.Empty,
                hexaPackets.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                    )
                );

            queue = new Queue<string>();
            
            foreach (char c in binaryPackets)
                queue.Enqueue(c.ToString());

            int total = 0;

            while (queue.Count() > 0)
            {
                string versionString = dequeueToString(3);
                string typeIDString = dequeueToString(3);
                int version = Convert.ToInt32(versionString, 2);
                int typeID = Convert.ToInt32(typeIDString, 2);
                total += version;

                // Literal value
                if (typeID == 4)
                {
                    string literalValueBits = "";
                    string fiveBits = dequeueToString(5);
                    int discardBits = 1;
                    do
                    {
                        literalValueBits += fiveBits[1..fiveBits.Length];
                        fiveBits = dequeueToString(5);
                        discardBits += 1;
                    } while (fiveBits[0] != '0');
                    literalValueBits += fiveBits[1..fiveBits.Length];
                    int literalValue = Convert.ToInt32(literalValueBits, 2);
                    _ = dequeueToString(discardBits % 4);
                } else 
                // Operator
                {
                    string lengthType = queue.Dequeue();
                    if (lengthType == "0")
                    {
                        string subPacketsLengthBits = dequeueToString(15);
                        int subPacketsLength = Convert.ToInt32(subPacketsLengthBits, 2);
                        _ = dequeueToString(subPacketsLength);
                    } else
                    {
                        string numberOfSubPacketsBits = dequeueToString(11);
                        int numberOfSubPackets = Convert.ToInt32(numberOfSubPacketsBits, 2);
                        for (int i = 0; i < numberOfSubPackets;i++)
                            _ = dequeueToString(11);

                    }
                }                
            }

            Console.WriteLine("Result: " + total);
        }

        public static string dequeueToString(int i)
        {
            return String.Join("", queue.DequeueChunk(i).ToArray());
        }
    }

    public static class QueueExtensions
    {
        public static IEnumerable<T> DequeueChunk<T>(this Queue<T> queue, int chunkSize)
        {
            for (int i = 0; i < chunkSize && queue.Count > 0; i++)
            {
                yield return queue.Dequeue();
            }
        }
    }
}
