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

        Rectangle view = new Rectangle(0, 0, 1600,950);
        Rectangle tex = new Rectangle(0, 0, 128, 128);
        Image grass = LoadImage("../Images/Environment/grass.png");

        Image alpha = GenImagePerlinNoise(128, 128, 50, 50, 4.0f);
        Image dirt = LoadImage("../Images/Environment/dirt.png");

        Texture2D background;
        Texture2D overlay;

        public Game()
        {
            ImageAlphaMask(ref dirt,alpha);

            background = LoadTextureFromImage(grass);
            overlay = LoadTextureFromImage(dirt);

            playerController = new PlayerController(
                new Tank(
                    new  MFG.Vector3(320,475,1),
                    new  MFG.Vector3(0,0,0),
                    -(float)Math.PI/2, 0, TANK_BLACK, Scene));
            new Tank(new MFG.Vector3(1280, 475, 1),
                    new MFG.Vector3(0, 0, 0),
                    (float)Math.PI / 2, 0, TANK_RED, Scene);

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
            LastTime = lastTime;
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

            Scene.PersonalRecursive();
            Scene.PhysicsRecursive();

            CollisionChecks();
            CollisionProcess();

            Scene.LocalTransformsRecursive();
            Scene.GlobalTransformsRecursive();

            BeginDrawing();
            ClearBackground(Color.LIGHTGRAY);
            DrawBackground();
            Scene.DrawRecursive();
            Scene.DrawDebugRecursive();
            EndDrawing();
        }

        public void DrawBackground()
        {
            DrawTextureQuad(background, new Vector2(1,1), Vector2.Zero, view, Color.WHITE);
            DrawTextureQuad(overlay, new Vector2(1,1), Vector2.Zero, view, Color.WHITE);
        }
    }
}
