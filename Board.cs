public static class Board
{
    private const int WIDTH = 125;
    private const int HEIGHT = 30;
    private static string horizontalLine = new String(char.Parse("#"), WIDTH);

    private static string verticalLines = "#" + new String(char.Parse(" "), WIDTH - 2) + "#";

    private static string paddle = "|";

    public static void DrawBoundary()
    {
        Console.WriteLine(horizontalLine);
        for (int i = 0; i < HEIGHT; i++)
        {
            Console.WriteLine(verticalLines);
        }
        Console.WriteLine(horizontalLine);
    }

    public static void DrawPaddles()
    {
        Console.SetCursorPosition(1, HEIGHT / 2 + 1);
        Console.Write(paddle);
    }
}
