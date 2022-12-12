public static class Board
{
    private const int WIDTH = 125;
    private const int HEIGHT = 30;
    private static string horizontalLine = new String(char.Parse("#"), WIDTH);

    private static string verticalLines = "#" + new String(char.Parse(" "), WIDTH - 2) + "#";

    private static string paddle = "|";

    private static int leftPaddleHeight = HEIGHT / 2 + 1;

    private static int rightPaddleHeight = HEIGHT / 2 + 1;

    public static void DrawBoundary()
    {
        Console.WriteLine(horizontalLine);
        for (int i = 0; i < HEIGHT; i++)
        {
            Console.WriteLine(verticalLines);
        }
        Console.WriteLine(horizontalLine);
    }

    public static void DrawPaddleLeft()
    {
        int x = Console.CursorLeft;
        int y = Console.CursorTop;
        Console.SetCursorPosition(1, leftPaddleHeight);
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine(paddle);
            Console.SetCursorPosition(1, leftPaddleHeight + i);
        }
        Console.SetCursorPosition(x, y);
    }

    public static void DrawPaddleRight()
    {
        int x = Console.CursorLeft;
        int y = Console.CursorTop;

        Console.SetCursorPosition(WIDTH - 2, rightPaddleHeight);
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine(paddle);
            Console.SetCursorPosition(WIDTH - 2, rightPaddleHeight + i);
        }
        Console.SetCursorPosition(x, y);
    }

    public static void HandleMovement()
    {
        while (!Console.KeyAvailable) { }

        switch (Console.ReadKey(false).Key)
        {
            case ConsoleKey.P:
                if (rightPaddleHeight > 0)
                {
                    rightPaddleHeight--;
                }
                break;
            case ConsoleKey.L:
                if (rightPaddleHeight < HEIGHT)
                {
                    rightPaddleHeight++;
                }
                break;
            case ConsoleKey.Q:
                if (leftPaddleHeight > 0)
                {
                    leftPaddleHeight--;
                }
                break;
            case ConsoleKey.A:
                if (leftPaddleHeight < HEIGHT)
                {
                    leftPaddleHeight++;
                }
                break;
        }
    }
}
