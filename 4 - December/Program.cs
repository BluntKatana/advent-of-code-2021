using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;

namespace Collatz
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> winNumbers = new List<int>();
            string firstRow = Console.ReadLine();
            foreach (string s in firstRow.Split(","))
                winNumbers.Add(Int32.Parse(s));

            List<BingoCard> bingoCards = new List<BingoCard>();

            // Parse the input into bingocards
            string currLine = Console.ReadLine();
            while (currLine != ":q")
            {
                // Gather all the Cell Arrays and converts to BingoCard
                Cell[] row1 = toCellArray(Console.ReadLine());
                Cell[] row2 = toCellArray(Console.ReadLine());
                Cell[] row3 = toCellArray(Console.ReadLine());
                Cell[] row4 = toCellArray(Console.ReadLine());
                Cell[] row5 = toCellArray(Console.ReadLine());
                
                bingoCards.Add(new BingoCard(row1, row2, row3, row4, row5, false));

                // Checks for new line
                currLine = Console.ReadLine();
            }

            int totalBingos = bingoCards.Count;
            // Do the checks
            foreach (int check in winNumbers)
            {
                foreach (BingoCard card in bingoCards)
                {
                    if (!card.bingo)
                    {
                        card.row1 = checkForNumber(card.row1, check);
                        card.row2 = checkForNumber(card.row2, check);
                        card.row3 = checkForNumber(card.row3, check);
                        card.row4 = checkForNumber(card.row4, check);
                        card.row5 = checkForNumber(card.row5, check);

                        if (card.CheckForBingo())
                        {
                            card.bingo = true;
                            totalBingos--;

                            if (totalBingos < 1)
                            {
                                Console.WriteLine("bingo " + card.calcResult(check));
                                return;
                            }
                        }
                    } 
                }
            }

            Console.WriteLine("Result");
        }

        static Cell[] toCellArray(string s)
        {
            List<Cell> res = new List<Cell>();
            string[] res_string = s.Split(" ");

            foreach (string res_s in res_string)
                if (res_s != "" && res_s != null && res_s != " ")
                    res.Add(new Cell(int.Parse(res_s), false));

            return res.ToArray();

        }

        static Cell[] checkForNumber(Cell[] row, int checkNum)
        {
            Cell[] res = new Cell[5];
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i].num == checkNum)
                    res[i] = new Cell(row[i].num, true);
                else
                    res[i] = new Cell(row[i].num, row[i].check);
            }
            return res;
        }
    }

    class Cell
    {
        public bool check;
        public int num;
        public Cell(int numI, bool checkI)
        {
            num = numI;
            check = checkI;
        }
    }

    class BingoCard
    {
        public Cell[] row1;
        public Cell[] row2;
        public Cell[] row3;
        public Cell[] row4;
        public Cell[] row5;

        public bool bingo;

        public BingoCard(Cell[] row1t, Cell[] row2t, Cell[] row3t, Cell[] row4t, Cell[] row5t, bool bingot)
        {
            row1 = row1t;
            row2 = row2t;
            row3 = row3t;
            row4 = row4t;
            row5 = row5t;
            bingo = bingot;
        }

        public bool CheckForBingo()
        {

            // check rows for bingo
            if (checkRow(row1) || checkRow(row2) ||
                checkRow(row3) || checkRow(row4) || checkRow(row5))
                return true;
            // check columns for bingo
            if (checkColumn(row1[0], row2[0], row3[0], row4[0], row5[0]) ||
                checkColumn(row1[1], row2[1], row3[1], row4[1], row5[1]) ||
                checkColumn(row1[2], row2[2], row3[2], row4[2], row5[2]) ||
                checkColumn(row1[3], row2[3], row3[3], row4[3], row5[3]) ||
                checkColumn(row1[4], row2[4], row3[4], row4[4], row5[4]))
                return true;

            return false;
        }

        static bool checkRow(Cell[] row)
        {
            int trues = 0;
            foreach (Cell r in row)
                if (r.check)
                    trues++;
            if (trues == 5)
                return true;
            return false;
        }

        static bool checkColumn(Cell one, Cell two, Cell three, Cell four, Cell five)
        {
            if (one.check && two.check && three.check && three.check && four.check && five.check)
                return true;
            else
                return false;
        }

        public int calcResult(int winNum)
        {
            int sumUnmarked = 0;
            sumUnmarked += calcRow(row1);
            sumUnmarked += calcRow(row2);
            sumUnmarked += calcRow(row3);
            sumUnmarked += calcRow(row4);
            sumUnmarked += calcRow(row5);

            return (sumUnmarked * winNum);
        }

        static int calcRow(Cell[] row)
        {
            int res = 0;
            foreach (Cell r in row)
                if (!r.check)
                    res += r.num;
            return res;
        }
    }

}
