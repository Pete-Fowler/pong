public static class GameLoop
{
    public static async void Run()
    {
        Board.DrawBoundary();
        await IntroText();

        while (true)
        {
            Board.DrawPaddleLeft();
            Board.DrawPaddleRight();

            Board.HandleMovement();
        }
    }

    public static async Task IntroText()
    {
        Console.Write("Welcome to ");
        Thread.Sleep(500);

        Console.Write("P");
        Thread.Sleep(750);

        Console.Write(" O");
        Thread.Sleep(750);

        Console.Write(" N");
        Thread.Sleep(750);

        Console.Write(" G");
        Thread.Sleep(750);

        Console.WriteLine(" ");

        Console.WriteLine("Left player moves up/down with q/a buttons ...");
        Console.WriteLine("Right player moves up/down with p/l buttons ...");
        Console.WriteLine("Press ESC to end game.");
    }
}
