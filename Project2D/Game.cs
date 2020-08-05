using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using static GraphicalTest.Globals;

namespace GraphicalTest
{
    class Game
    {
        Stopwatch stopwatch = new Stopwatch();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private float deltaTime = 0.005f;
        PlayerController player;

        Image logo;
        Texture2D texture;

        public Game()
        {
            player = new PlayerController(new Tank(0, 0, 0, 0));
        }

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }

            //logo = LoadImage("..\\Images\\aie-logo-dark.jpg");
            //logo = LoadImage(@"..\Images\aie-logo-dark.jpg");
            logo = LoadImage("../Images/aie-logo-dark.jpg");
            texture = LoadTextureFromImage(logo);
        }

        public void Shutdown()
        {
        }

        public void UpdateGame()
        {
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;

            Globals.DeltaTime = deltaTime;

            allTanks.ForEach(t => t.Update());
            allTurrets.ForEach(t => t.Update());
            allBullets.ForEach(t => t.Update());
        }

        public void Draw()
        {
            BeginDrawing();

            ClearBackground(Color.LIGHTGRAY);

            DrawText(fps.ToString(), 10, 10, 14, Color.RED);                                      

            EndDrawing();
        }
    }
}
