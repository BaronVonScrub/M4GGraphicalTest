using static Raylib.Raylib;

namespace GraphicalTest
{
    class Program
    {
        static void Main(string[] args)
        {
            InitWindow(1600, 950, "Hello World");
            Game game = new Game();
            game.Init();

            while (!WindowShouldClose())
            {
                game.UpdateGame();
            }

            game.Shutdown();

            CloseWindow();
        }
    }
}
