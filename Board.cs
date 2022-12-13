using System.ComponentModel.DataAnnotations;

public static class Board
{
    private const int WIDTH = 125;
    private const int HEIGHT = 30;
    private static string horizontalLine = new String(char.Parse("#"), WIDTH);

    private static string verticalLines = "#" + new String(char.Parse(" "), WIDTH - 2) + "#";

    private static string paddle = "|";

    private static int leftPaddleHeight = HEIGHT / 2 - 2;

    private static int rightPaddleHeight = HEIGHT / 2 - 2;

    private static int ballX = WIDTH / 2;
    private static int ballY = HEIGHT / 2;
    private const char ballTile = 'O';
    private static int ballSpeed = 100;

    private static bool isBallGoingDown = true;
    private static bool isBallGoingRight = true;

    public static void DrawBoundary()
    {
        Console.WriteLine(horizontalLine);
        Thread.Sleep(100);
        for (int i = 0; i < HEIGHT; i++)
        {
            Console.WriteLine(verticalLines);
            Thread.Sleep(50);
        }
        Console.WriteLine(horizontalLine);
    }

    public static void DrawPaddles()
    {
        int x = Console.CursorLeft;
        int y = Console.CursorTop;
        for (int i = 0; i <= 4; i++)
        {
            Console.SetCursorPosition(1, leftPaddleHeight + i);
            Console.WriteLine(paddle);
            Console.SetCursorPosition(WIDTH - 2, rightPaddleHeight + i);
            Console.WriteLine(paddle);
        }
        Console.SetCursorPosition(x, y);
    }

    public static void Goal(string side)
    {
        Console.WriteLine($"{side} player GOAL");
    }

    public static void HandleMovement()
    {
        Console.CursorVisible = false;
        while (!Console.KeyAvailable)
        {
            Console.SetCursorPosition(ballX, ballY);
            Console.Write(ballTile);
            Thread.Sleep(ballSpeed);
            Console.SetCursorPosition(ballX, ballY);
            Console.Write(" ");

            if (isBallGoingDown)
            {
                if (ballY < HEIGHT - 1)
                {
                    ballY++;
                }
                else if (ballY == HEIGHT - 1)
                {
                    isBallGoingDown = !isBallGoingDown;
                }
            }
            else if (!isBallGoingDown)
            {
                if (ballY > 2)
                {
                    ballY--;
                }
                else if (ballY == 1)
                {
                    isBallGoingDown = !isBallGoingDown;
                }
            }
            if (isBallGoingRight)
            {
                if (ballX < WIDTH - 2)
                {
                    ballX++;
                }
                else if (ballX == WIDTH - 2)
                {
                    Goal("Left");
                }
            }
            else if (!isBallGoingRight)
            {
                if (ballX > WIDTH + 1)
                {
                    ballX--;
                }
                else if (ballX == WIDTH + 1)
                {
                    Goal("Right");
                }
            }
        }

        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.P:
                if (rightPaddleHeight > 1)
                {
                    rightPaddleHeight--;
                }
                break;
            case ConsoleKey.L:
                if (rightPaddleHeight < HEIGHT - 4)
                {
                    rightPaddleHeight++;
                }
                break;
            case ConsoleKey.Q:
                if (leftPaddleHeight > 1)
                {
                    leftPaddleHeight--;
                }
                break;
            case ConsoleKey.A:
                if (leftPaddleHeight < HEIGHT - 4)
                {
                    leftPaddleHeight++;
                }
                break;
        }
        for (int i = 1; i < HEIGHT + 1; i++)
        {
            Console.SetCursorPosition(1, i);
            Console.WriteLine(" ");
            Console.SetCursorPosition(WIDTH - 2, i);
            Console.WriteLine(" ");
        }
    }
}
