using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string playerOne = Console.ReadLine();
            string playerTwo = Console.ReadLine();

            Player one = new Player(playerOne[playerOne.Length - 1] - '0', 0, 0);
            Player two = new Player(playerTwo[playerTwo.Length - 1] - '0', 0, 0);

            List<Player> players = new List<Player>() { one, two };

            foreach (int die in diceThrows())
                doTurn(players, 0, die);

            Console.WriteLine("Player 1: " + players[0].wins);
            Console.WriteLine("Player 2: " + players[1].wins);
        }
        public static void doTurn(List<Player> players, int currP, int die)
        {

            players[currP % 2].doMove(die);

            // Check for win
            if (players[currP % 2].score >= 21)
            {
                // Add state to cache
                players[currP % 2].wins++;
                return;
            }

            // Do all the other possible moves
            foreach (int i in diceThrows()) 
                doTurn(players, currP % 2 + 1, i);
        }
        public static List<int> diceThrows()
        {
            List<int> distinctDiceThrows = new List<int>();
            for (int i = 1; i <= 3; i++)
                for (int j = 1; j <= 3; j++)
                    for (int k = 1; k <= 3; k++)
                        distinctDiceThrows.Add(i + j + k);

            return distinctDiceThrows;

        }
    }

    class Player
    {
        public long position, score, wins;
        public Player(int position, int score, int wins)
        {
            this.position   = position;
            this.score      = score;
            this.wins       = wins;
        }

        public void doMove(int throws)
        {
            // all them rolls
            position += throws;

            // do wrap around
            while (position > 10)
                position -= 10;

            // add score
            score += position;
        }
    }
}
