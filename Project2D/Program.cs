using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using static GraphicalTest.Sprites;
using System.IO;

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
