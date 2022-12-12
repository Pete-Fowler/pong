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
        Thread.Sleep(100);
        for (int i = 0; i < HEIGHT; i++)
        {
            Console.WriteLine(verticalLines);
            Thread.Sleep(75);
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
