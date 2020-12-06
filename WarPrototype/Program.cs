using System;
using System.Windows.Input;
using System.Collections.Generic;

namespace WarPrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Deal();
            game.SetHealth();
            game.Play();

            
        }
    }

    public class Game
    {
        public int PlayerHealth { get; private set; }
        public int CpuHealth { get; private set; }
        public bool IsGameOver { get; private set; }

        public List<int> deck = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

        public int[] playerCards = new int[7];
        public int[] cpuCards = new int[7];


        public void Deal()
        {
            Random random = new Random();
            for (int i = 0; i < playerCards.Length; i++)
            {
                int cardIndex = random.Next(0, deck.Count - 1);
                playerCards[i] = deck[cardIndex];
                deck.RemoveAt(cardIndex);
            }

            for (int i = 0; i < deck.Count; i++)
            {
                cpuCards[i] = deck[i];
            }

            deck.Clear();
        }

        public void SetHealth()
        {
            PlayerHealth = 10;
            CpuHealth = 10;
        }


        //game instance
        public void Play()
        {
            for (int i = 0; i < playerCards.Length; i++)
            {
                ShowRound(i);


                int bet = 0;
                ConsoleKeyInfo key;
                while (bet < 1)
                {
                    key = Console.ReadKey(true);
                    int k = (int)Char.GetNumericValue(key.KeyChar);
                    if (k != 0 && k <= PlayerHealth && k >= 1 && k <= 5)
                    {
                        bet = k;
                    }
                    else
                    {
                        bet = 0;
                    }

                }

                DisplayCpuCard(i);

                if (playerCards[i] > cpuCards[i])
                {
                    Console.WriteLine("Player wins round!");
                    CpuHealth -= bet;
                }
                else
                {
                    Console.WriteLine("CPU wins round!");
                    PlayerHealth -= bet;
                }

                DisplayGameOver(); //needs to check health at end of match

                if (IsGameOver) return;

                Console.ReadKey();
            }
        }

        private void DisplayCpuCard(int i)
        {
            Console.WriteLine();
            Console.WriteLine("CPU card: " + cpuCards[i]);
            Console.WriteLine();
        }

        private void DisplayGameOver()
        {
            if (PlayerHealth <= 0)
            {
                Console.Clear();
                Console.WriteLine("You have been defeated...");
                Console.WriteLine("Player loses war!!!");
                IsGameOver = true;
            }
            else if (CpuHealth <= 0)
            {
                Console.Clear();
                Console.WriteLine("CPU been defeated...");
                Console.WriteLine("CPU loses war!!!");
                IsGameOver = true;
            }
        }

        private void ShowRound(int i)
        {
            Console.Clear();
            Console.WriteLine(playerCards[i]);
            Console.WriteLine();
            Console.WriteLine("Player health: " + PlayerHealth + "                   " + "CPU health: " + CpuHealth);
            Console.WriteLine("Bet?");
        }
    }


}
