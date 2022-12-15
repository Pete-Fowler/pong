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
    private static int ballSpeed = 60;
    private static bool isBallSpeedReduced = false;

    private static bool isBallGoingDown = true;
    private static bool isBallGoingRight = false;
    private static int ballAngle = 0;

    private static int rightScore = 0;
    private static int leftScore = 0;

    private static bool running = true;

    public static void HandleMovement()
    {
        Console.CursorVisible = false;
        while (!Console.KeyAvailable && running)
        {
            AdjustBallSpeed();
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
        if (1 < ballY && ballY < HEIGHT)
        {
            Console.Write(ballTile);
            Thread.Sleep(ballSpeed);
            Console.SetCursorPosition(ballX, ballY);
            Console.Write(" ");
        }
        else
        {
            Thread.Sleep(ballSpeed);
        }
    }

    private static void HandleBallAtBorders()
    {
        if (isBallGoingDown)
        {
            if (ballY < HEIGHT)
            {
                ballY += ballAngle;
            }
            else if (ballY >= HEIGHT)
            {
                isBallGoingDown = !isBallGoingDown;
            }
        }
        else if (!isBallGoingDown)
        {
            if (ballY > 1)
            {
                ballY -= ballAngle;
            }
            else if (ballY <= 1)
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
                if (rightPaddleHeight <= ballY && ballY <= rightPaddleHeight + 4)
                {
                    isBallGoingRight = !isBallGoingRight;
                    AdjustBallAngle("Right");
                    if (ballSpeed >= 20)
                    {
                        ballSpeed -= 5;
                    }
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
                if (leftPaddleHeight <= ballY && ballY <= leftPaddleHeight + 4)
                {
                    isBallGoingRight = !isBallGoingRight;
                    AdjustBallAngle("Left");
                    if (ballSpeed >= 20)
                    {
                        ballSpeed -= 5;
                    }
                }
                else
                {
                    Goal("Right");
                }
            }
        }
    }

    private static void AdjustBallAngle(string side)
    {
        int paddleHeight = 0;
        if (side == "Left")
        {
            paddleHeight = leftPaddleHeight;
        }
        else
        {
            paddleHeight = rightPaddleHeight;
        }

        int ballDeltaPaddle = ballY - paddleHeight;

        switch (ballDeltaPaddle)
        {
            case 0:
                ballAngle = 2;
                isBallGoingDown = false;
                break;
            case 1:
                ballAngle = 1;
                isBallGoingDown = false;
                break;
            case 2:
                ballAngle = 0;
                break;
            case 3:
                ballAngle = 1;
                isBallGoingDown = true;
                break;
            case 4:
                ballAngle = 2;
                isBallGoingDown = true;
                break;
        }
        //if (ballY == paddleHeight || ballY == paddleHeight + 4)
        //{
        //    ballAngle = 2;
        //}
        //else if (ballY == paddleHeight + 1 || ballY == paddleHeight + 3)
        //{
        //    ballAngle = 1;
        //}
        //else if (ballY == paddleHeight + 2)
        //{
        //    ballAngle = 0;
        //}
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
        ballAngle = 0;
        ballSpeed = 60;
        isBallSpeedReduced = false;
    }

    private static void AdjustBallSpeed()
    {
        if (ballAngle == 2 && isBallSpeedReduced == false)
        {
            ballSpeed *= 2;
            isBallSpeedReduced = true;
        }
        else if (ballAngle != 2 && isBallSpeedReduced == true)
        {
            ballSpeed /= 2;
            isBallSpeedReduced = false;
        }
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

                string scoreBoard = $"Left Player: {leftScore} | Right Player: {rightScore}";
                ClearString(scoreBoard, WIDTH / 2 - scoreBoard.Length / 2, HEIGHT + 2);

                ClearPaddles();
                leftPaddleHeight = HEIGHT / 2 - 2;
                rightPaddleHeight = HEIGHT / 2 - 2;

                running = true;
                break;

            case ConsoleKey.N:
                Environment.Exit(1);
                break;
        }

    }
}
