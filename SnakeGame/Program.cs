using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace SnakeGame
{
    class SnakeGame
    {
        private static int ScreenWidth = Console.WindowWidth = 120;
        private static int ScreenHeight = Console.WindowHeight = 30;


        private static Queue<(int X, int Y)> Snake;
        private static (int X, int Y) Food;
        private static bool IsGameOver;
        private static int Score;
        public static void Main()
        {
            Score = 3;
            Console.CursorVisible = false;
            Console.SetWindowSize(ScreenWidth, ScreenHeight);

            Snake = new Queue<(int X, int Y)>(Enumerable.Range(0, Score).Select(i => (ScreenWidth / 2 + i, ScreenHeight / 2)));
            Food = GenerateFoodPosition();
            IsGameOver = false;

            while (IsGameOver == false)
            {
                char ch = Console.ReadKey(true).KeyChar;
                DrawGame(ch);
                UpdateSnake(ch);
                CheckForGameOver();
                Thread.Sleep(25);
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(Score);
            Console.ReadKey();
        }

        public static void DrawGame(char ch)
        {
            Console.Clear();
            // Draw the snake
            foreach (var (x, y) in Snake)
            {
                Console.Clear();
                Console.SetCursorPosition(x, y);
                try
                {
                    switch (ch)
                    {
                        case 'w':
                            for (int j = 1; j <= Score; j++)
                            {
                                if (j == Score)
                                {
                                    Console.SetCursorPosition(x, y - (Score + 1));
                                    Console.WriteLine("0");
                                }
                                Console.SetCursorPosition(x, y - j);
                                Console.WriteLine("*");
                            }
                            break;
                        case 's':
                            for (int k = 1; k <=Score ; k++)
                            {
                                if(k == Score)
                                {
                                    Console.SetCursorPosition(x, y);
                                    Console.WriteLine("0");
                                }
                                Console.SetCursorPosition(x, y - k);
                                Console.WriteLine("*");
                            }
                            break;
                        case 'a':
                            for (int m = 1; m <= Score; m++)
                            {
                                if (m == Score)
                                {
                                    Console.SetCursorPosition(x - (Score +1), y);
                                    Console.WriteLine("0");
                                }
                                Console.SetCursorPosition(x-m, y);
                                Console.WriteLine("*");
                            }
                            break;
                        case 'd':
                            for (int n = 1; n <= Score; n++)
                            {
                                if (n == Score)
                                {
                                    Console.SetCursorPosition(x + (Score-1),y);
                                    Console.WriteLine("0");
                                }
                                Console.SetCursorPosition(x+n-2, y);
                                Console.WriteLine("*");
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                     IsGameOver = true;
                }
            }

            // Draw the food
            Console.SetCursorPosition(Food.X, Food.Y);
            Console.Write("*");
        }

        public static void UpdateSnake(char ch)
        {
            // Calculate the new head position
            var head = Snake.Last();
            int newHeadX, newHeadY;
            newHeadX = head.X;
            newHeadY = head.Y;
            switch (ch)
            {
                case 'd':
                    newHeadX += 1;
                    break;
                case 'a':
                    newHeadX -= 1;
                    break;
                case 'w':
                    newHeadY -= 1;
                    break;
                case 's':
                    newHeadY += 1;
                    break;
                case 'q':
                    IsGameOver = true;
                    break;
                default:
                    break;
            }
            Snake.Enqueue((newHeadX % ScreenWidth, newHeadY % ScreenHeight));

            // If the snake has eaten the food, grow the snake
            if (Snake.Last().Equals(Food))
            {
                Score++;
                Food = GenerateFoodPosition();
            }
            else
            {
                Snake.Dequeue();
            }
        }

        public static void CheckForGameOver()
        {
            // Check for collision with the wall
            var head = Snake.Last();
            if (head.X < 0 || head.X > ScreenWidth || head.Y < 0 || head.Y > 28)
            {
                IsGameOver = true;
            }

            // Check for collision with the snake's body
            foreach (var segment in Snake)
            {
                if (!segment.Equals(head) && segment.Equals(head))
                {
                    IsGameOver = true;
                }
            }
        }

        public static (int X, int Y) GenerateFoodPosition()
        {
            var random = new Random();
            return (random.Next(ScreenWidth), random.Next(ScreenHeight));
        }
    } 
}
