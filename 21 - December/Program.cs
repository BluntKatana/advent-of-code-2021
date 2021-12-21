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

            doTurn(players, 1, 0);
        }
        public static void doTurn(List<Player> players, int die, int currP)
        {
            players[currP % 2].doMove(die, die + 1, die + 2);

            // Check for win
            if (players[currP % 2].score >= 1000)
            {
                long res = die + 2;
                Console.WriteLine("Result: " + (res * players[currP % 2 + 1].score).ToString());
                return;
            }

            doTurn(players, die + 3, currP % 2 + 1);
        }

    }



    class Player
    {
        public int position, score, wins;
        public Player(int position, int score, int wins)
        {
            this.position   = position;
            this.score      = score;
            this.wins       = wins;
        }

        public void doMove(int firstRoll, int secondRoll, int thirdRoll)
        {
            // all them rolls
            position += firstRoll + secondRoll + thirdRoll;

            // do wrap around
            while (position > 10)
                position -= 10;

            // add score
            score += position;
        }
    }
}
