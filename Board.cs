public static class Board
{
    private const int WIDTH = 125;
    private const int HEIGHT = 30;
    private static string line = new String(char.Parse("#"), WIDTH);

    public static void Draw()
    {
        Console.WriteLine(line);
        for (int i = 0; i < HEIGHT; i++)
        {
            Console.WriteLine("#" + new String(char.Parse(" "), WIDTH - 2) + "#");
        }
        Console.WriteLine(line);
    }
}
