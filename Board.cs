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
    private static int ballSpeed = 30;

    private static bool isBallGoingDown = true;
    private static bool isBallGoingRight = false;

    private static int rightScore = 0;
    private static int leftScore = 0;

    private static bool running = true;

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

    private static void ClearPaddles()
    {
        for (int i = 1; i < HEIGHT + 1; i++)
        {
            Console.SetCursorPosition(1, i);
            Console.WriteLine(" ");
            Console.SetCursorPosition(WIDTH - 2, i);
            Console.WriteLine(" ");
        }
    }

    private static void DrawBall()
    {
        Console.SetCursorPosition(ballX, ballY);
        Console.Write(ballTile);
        Thread.Sleep(ballSpeed);
        Console.SetCursorPosition(ballX, ballY);
        Console.Write(" ");
    }

    private static void HandleBallAtBorders()
    {
        if (isBallGoingDown)
        {
            if (ballY < HEIGHT)
            {
                ballY++;
            }
            else if (ballY == HEIGHT)
            {
                isBallGoingDown = !isBallGoingDown;
            }
        }
        else if (!isBallGoingDown)
        {
            if (ballY > 1)
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
            if (ballX < WIDTH - 3)
            {
                ballX++;
            }
            else if (ballX == WIDTH - 3)
            {
                if (rightPaddleHeight <= ballY && ballY <= rightPaddleHeight + 5)
                {
                    isBallGoingRight = !isBallGoingRight;
                }
                else
                {
                    Goal("Left");
                }
            }
        }
        else if (!isBallGoingRight)
        {
            if (ballX > 2)
            {
                ballX--;
            }
            else if (ballX == 2)
            {
                if (leftPaddleHeight <= ballY && ballY <= leftPaddleHeight + 5)
                {
                    isBallGoingRight = !isBallGoingRight;
                }
                else
                {
                    Goal("Right");
                }
            }
        }
    }

    public static void Goal(string side)
    {
        running = false;

        if (side == "Left")
        {
            leftScore++;
            if (leftScore == 5)
            {
                WinGame("Left");
                return;
            }
        }
        else
        {
            rightScore++;
            if (rightScore == 5)
            {
                WinGame("Right");
                return;
            }
        }

        var str = $"{side} Player GOAL!!!";

        Console.SetCursorPosition((WIDTH / 2) - (str.Length / 2), HEIGHT / 2);
        Console.WriteLine(str);
        Thread.Sleep(1000);

        string scoreBoard = $"Left Player: {leftScore} | Right Player: {rightScore}";
        Console.SetCursorPosition((WIDTH / 2) - (scoreBoard.Length / 2), HEIGHT + 2);
        Console.WriteLine(scoreBoard);
        Thread.Sleep(1000);

        ResetBall();

        ClearString(str, (WIDTH / 2) - (str.Length / 2), HEIGHT / 2);

        running = true;
    }

    private static void ClearString(string str, int x, int y)
    {
        for (int i = 0; i < str.Length; i++)
        {
            Console.SetCursorPosition(x + i, y);
            Console.Write(" ");
        }
    }

    private static void ResetBall()
    {
        ballX = WIDTH / 2;
        ballY = HEIGHT / 2;
    }

    public static void HandleMovement()
    {
        Console.CursorVisible = false;
        while (!Console.KeyAvailable && running)
        {
            DrawPaddles();
            DrawBall();
            HandleBallAtBorders();
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
        ClearPaddles();
    }

    private static void WinGame(string side)
    {
        string str = $"{side} player is the winner!!!";
        Console.SetCursorPosition((WIDTH / 2) - (str.Length / 2), HEIGHT / 2);
        Console.Write(str);
        Thread.Sleep(2000);

        string instructions = "Press Y to play again or N to quit...";
        Console.SetCursorPosition((WIDTH / 2) - (instructions.Length / 2), HEIGHT / 2 + 2);
        Console.Write(instructions);

        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.Y:
                ClearString(str, (WIDTH / 2) - (str.Length / 2), HEIGHT / 2);
                ClearString(instructions, (WIDTH / 2) - (instructions.Length / 2), HEIGHT / 2 + 2);
                ResetBall();
                leftScore = 0;
                rightScore = 0;
                leftPaddleHeight = HEIGHT / 2 - 2;
                rightPaddleHeight = HEIGHT / 2 - 2;

                running = true;
                break;

            case ConsoleKey.N:
                Environment.Exit(-1);
                break;
        }

    }
}
