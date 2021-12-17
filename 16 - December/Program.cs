using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Program
    {
        public static long versionTotal = 0;
        static void Main(string[] args)
        {

            string hexaNums = Console.ReadLine();
            string binaryNums = String.Join(String.Empty,
                hexaNums.Select(
                    c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                    )
                );

            Queue<string> queue = new Queue<string>();
            foreach (char c in binaryNums)
                queue.Enqueue(c.ToString());

            long total = header(queue);
            Console.WriteLine("Result: " + total);
        }

        public static long header(Queue<string> queue)
        {
            long version = dequeueToInt(3, queue);
            long typeID = dequeueToInt(3, queue);

            // Part 1
            versionTotal += version;

            if (typeID == 4)
                return literalPacket(queue);
            else
                return operatorPacket(queue, typeID);
        }

        public static long literalPacket(Queue<string> queue)
        {
            long literalValue = 0;
            string checkBit = dequeueToString(1, queue);
            while (checkBit != "0")
            {
                long fourBits = dequeueToInt(4, queue);
                literalValue = literalValue * 16 + fourBits;
                checkBit = dequeueToString(1, queue);
            }
            return literalValue * 16 + dequeueToInt(4, queue);
        }

        public static long operatorPacket(Queue<string> queue, long typeID)
        {
            string lengthType = queue.Dequeue();
            List<long> subPacketTotals = new List<long>();

            if (lengthType == "0")
            {
                // Length based packets
                long subPacketsLength = dequeueToInt(15, queue);

                Queue<string> subPackets = new Queue<string>();
                string subPacketsString = dequeueToString(subPacketsLength, queue);
                foreach (char c in subPacketsString)
                    subPackets.Enqueue(c.ToString());

                while (subPackets.Count() > 0)
                    subPacketTotals.Add(header(subPackets));
            }
            else
            {
                // Number based packets
                long numberOfSubPackets = dequeueToInt(11, queue);
                for (long i = 0; i < numberOfSubPackets; i++)
                    subPacketTotals.Add(header(queue));
            }

            switch (typeID) {
                case 0:
                    return subPacketTotals.Sum();
                case 1:
                    return subPacketTotals.Aggregate((total, next) => total * next);
                case 2:
                    return subPacketTotals.Min();
                case 3:
                    return subPacketTotals.Max();
                case 5:
                    return subPacketTotals[0] > subPacketTotals[1] ? 1 : 0;
                case 6:
                    return subPacketTotals[0] < subPacketTotals[1] ? 1 : 0;
                default: // case 7
                    return subPacketTotals[0] == subPacketTotals[1] ? 1 : 0;
            }
        }


        public static string dequeueToString(long i, Queue<string> queue)
        {
            return String.Join("", queue.DequeueChunk(i).ToArray());
        }

        public static long dequeueToInt(long i, Queue<string> queue)
        {
            return Convert.ToInt32(String.Join("", queue.DequeueChunk(i).ToArray()), 2);
        }
    }

    public static class QueueExtensions
    {
        public static IEnumerable<T> DequeueChunk<T>(this Queue<T> queue, long chunkSize)
        {
            for (long i = 0; i < chunkSize && queue.Count > 0; i++)
            {
                yield return queue.Dequeue();
            }
        }
    }
}
