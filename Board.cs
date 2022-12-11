public static class Board
{
    private const int WIDTH = 75;
    private const int HEIGHT = 40;
    private static string line = string.Join("", Enumerable.Repeat('=', WIDTH));

    public static void Draw()
    {
        Console.WriteLine(line);
    }
}
