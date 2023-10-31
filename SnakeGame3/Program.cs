using Microsoft.Win32.SafeHandles;
using System;
using System.Drawing;
using System.Linq;

class Program
{
    private static int ScreenWidth = Console.WindowWidth = 120;
    private static int ScreenHeight = Console.WindowHeight = 30;
    private static bool IsGameOver;
    private static int Score;
    private static (int X, int Y) Food;

    public static void Main()
    {
        Score = 1;
        Console.CursorVisible = false;
        var snake = new Snake(new Point(Console.WindowWidth / 2, Console.WindowHeight / 2), Score, '0');

        Food = GenerateFoodPosition();
        Console.SetCursorPosition(Food.X, Food.Y);
        Console.WriteLine("*");

        while (IsGameOver == false){
            var ch = Console.ReadKey(true).Key;
            if (ch == ConsoleKey.Q) break;
            switch (ch)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    snake.Move(Snake.Direction.Up);
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    snake.Move(Snake.Direction.Down);
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    snake.Move(Snake.Direction.Left);
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    snake.Move(Snake.Direction.Right);
                    break;
            }
            if (snake.Head.Position.X == Food.X && snake.Head.Position.Y == Food.Y)
            {
                Food = GenerateFoodPosition();
                Console.SetCursorPosition(Food.X, Food.Y);
                Console.WriteLine("*");
                Score++;
                snake.Grow();
            }
            else
                snake.Draw();
            for (int i = 0;i<snake._body.Length;i++)
                for (int j = i+1+1; j < snake._body.Length; j++)
                    if (snake._body[i].Position.X == snake._body[j].Position.X && snake._body[i].Position.Y == snake._body[j].Position.Y)
                        IsGameOver = true;
        }
        Console.CursorVisible = true;
        Console.SetCursorPosition(0, 0);
        Console.Write("Well Done! Your Score : {0}\nPress enter...",Score);
        Console.ReadLine();

    }
    public class SnakePart
    {
        public char Symbol { get; set; }
        public Point Position { get; set; }

        public SnakePart(Point position, char symbol)
        {
            Position = position;
            Symbol = symbol;
        }

        public void Draw()
        {
            try
            {
                Console.SetCursorPosition(Position.X, Position.Y);
                Console.Write(Symbol);
            }
            catch (Exception)
            {
                IsGameOver = true;
            }
        }

        public void Erase()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(' ');
        }
    }
    public class Snake
    {
        public enum Direction { Up, Down, Left, Right }

        public char Symbol { get; set; }

        public SnakePart[] _body;
        public  SnakePart Head => _body.First();
        public SnakePart Tail => _body.Last();

        public Snake(Point startingPoint, int length, char symbol = '*')
        {
            Symbol = symbol;
            _body = Enumerable
                .Range(0, length)
                .Select(x => new SnakePart(startingPoint, symbol))
                .ToArray();
        }

        public void Draw()
        {
            foreach (var snakePart in _body)
            {
                snakePart.Draw();
            }
        }
        public void Grow()
        {
            var newTail = new SnakePart(Tail.Position, Symbol);
            _body = _body.Append(newTail).ToArray();
        }

        public void Move(Direction direction)
        {
            SnakePart newHead = null;
            switch (direction)
            {
                case Direction.Up:
                    newHead = new SnakePart(new Point(Head.Position.X,
                        Head.Position.Y - 1), Symbol);
                    break;
                case Direction.Down:
                    newHead = new SnakePart(new Point(Head.Position.X,
                        Head.Position.Y + 1), Symbol);
                    break;
                case Direction.Left:
                    newHead = new SnakePart(new Point(Head.Position.X - 1,
                        Head.Position.Y), Symbol);
                    break;
                case Direction.Right:
                    newHead = new SnakePart(new Point(Head.Position.X + 1,
                        Head.Position.Y), Symbol);
                    break;
            }
            Tail.Erase();
            for (var i = _body.Length - 1; i > 0; i--)
            {
                _body[i] = _body[i - 1];
            }
            _body[0] = newHead;
        }
    }
    public static (int X, int Y) GenerateFoodPosition()
    {
        var random = new Random();
        return (random.Next(ScreenWidth), random.Next(ScreenHeight));
    }
}
