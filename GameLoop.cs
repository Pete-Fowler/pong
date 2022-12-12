public static class GameLoop
{
    public static void Run()
    {
        IntroText();

        Board.DrawBoundary();

        while (true)
        {
            Board.DrawPaddleLeft();
            Board.DrawPaddleRight();

            Board.HandleMovement();
        }
    }

    public static void IntroText()
    {
        Console.Write("Welcome to ");
        Thread.Sleep(750);

        Console.Write("P");
        Thread.Sleep(750);

        Console.Write(" O");
        Thread.Sleep(750);

        Console.Write(" N");
        Thread.Sleep(750);

        Console.Write(" G");
        Thread.Sleep(750);

        Console.WriteLine(" ");

        Console.WriteLine("Left player use Q + A ...");
        Console.WriteLine("Right player use P + L ...");
        Console.WriteLine("Press Ctrl + C to end game.");

        Thread.Sleep(2500);
    }
}
