using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using static GraphicalTest.GlobalVariables;
using static GraphicalTest.Sprites;
using MFG = MathClasses;

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
        PlayerController playerController;

        public Game()
        {
            playerController = new PlayerController(
                new Tank(
                    new  MFG.Vector3(0,0,0),
                    new  MFG.Vector3(0,0,0),
                    0, 0, TANK_BLACK, Scene));
        }

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }

        }

        public void Shutdown()
        {
        }

        public void UpdateGame()
        {
            #region Time
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
            #endregion

            GlobalVariables.DeltaTime = deltaTime;

            playerController.ProcessInput();

            Scene.UpdateLocalTransforms();
            Scene.UpdateGlobalTransforms();

            BeginDrawing();
            ClearBackground(Color.LIGHTGRAY);
            Scene.Draw();
            EndDrawing();

        }
    }
}
