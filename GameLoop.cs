public static class GameLoop
{
    public static void Run()
    {
      
        Board.DrawBoundary();

        while (true)
        {
            Board.DrawPaddleLeft();
            Board.DrawPaddleRight();

            Board.HandleMovement();
        }
    }
}
