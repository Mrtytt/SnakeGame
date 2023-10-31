using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealSnakeGame
{
    public class Program
    {
        public const int SnakeSize = 3;
        public static int ScreenWidth = Console.WindowWidth;
        public static int ScreenHeight = Console.WindowHeight;

        public static (int X, int Y) Snake;
        public static (int X, int Y) Food;
        public static bool IsGameOver;
        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Snake = (new Random().Next(ScreenWidth),new Random().Next(ScreenHeight));

            Food = GenerateFoodPosition();
            IsGameOver = false;
        }
        public static (int X, int Y) GenerateFoodPosition()
        {
            var random = new Random();
            return (random.Next(ScreenWidth), random.Next(ScreenHeight));
        }
        public static void DrawGame()
        {
            Console.Clear();

            // Draw the snake
            foreach (var (x, y) in Snake)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("**0");
            }

            // Draw the food
            Console.SetCursorPosition(Food.X, Food.Y);
            Console.Write("*");
        }
    }
}
