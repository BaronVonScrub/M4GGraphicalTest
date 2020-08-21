using Raylib;
using System;
using System.Diagnostics;
using static GraphicalTest.CollisionManager;
using static GraphicalTest.Global;
using static GraphicalTest.Sprites;
using static Raylib.Raylib;
using MFG = MathClassesAidan;

namespace GraphicalTest
{
    class Game
    {
        //Time stuff
        Stopwatch stopwatch = new Stopwatch();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int frames;

        private float deltaTime = 0.005f;
        //

        //Preps a playercontroller for user interaction
        PlayerController playerController;

        //Sets the view as a rectangle for drawing background
        Rectangle view = new Rectangle(0, 0, 1600, 950);

        //Background image prep
        Image grass = LoadImage("../Images/Environment/grass.png");
        Image alpha = GenImagePerlinNoise(128, 128, 50, 50, 4.0f);
        Image dirt = LoadImage("../Images/Environment/dirt.png");
        Texture2D background;
        Texture2D overlay;
        //

        public Game()
        {
            //Background texture prep
            ImageAlphaMask(ref dirt, alpha);
            background = LoadTextureFromImage(grass);
            overlay = LoadTextureFromImage(dirt);
            //

            //Sets up the player controller with a new tank
            playerController = new PlayerController(
                new Tank(
                    new MFG.Vector3(320, 475, 1),
                    new MFG.Vector3(0, 0, 0),
                    -(float)Math.PI / 2, 0, TANK_BLACK, Scene));

            //Creates a new tank (Constructor adds to global Objectlist to maintain reference)
            new Tank(new MFG.Vector3(1280, 475, 1),
                    new MFG.Vector3(0, 0, 0),
                    (float)Math.PI / 2, 0, TANK_RED, Scene);
        }

        //Initializes the stopwatch
        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }
        }

        //Shutdown protocols
        public void Shutdown()
        {
        }

        //Runs on each game step
        public void UpdateGame()
        {
            //Time stuff
            #region Time
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            timer += deltaTime;
            if (timer >= 1)
            {
                frames = 0;
                timer -= 1;
            }
            frames++;
            #endregion
            //

            //Updates the globally accessible deltatime
            Global.DeltaTime = deltaTime;

            //Processes all player input
            playerController.ProcessInput();

            Scene.PersonalRecursive();                                          //Recursively process all personal matters of sceneobjects (E.g., cooldowns)
            Scene.PhysicsRecursive();                                           //Recursively process all physical matters of sceneobjects

            CollisionChecks();                                                  //Check for collisions
            CollisionProcess();                                                 //Process all collisions

            Scene.LocalTransformsRecursive();                                   //Recursively update all local transforms
            Scene.GlobalTransformsRecursive();                                  //Recursively update all global transforms

            BeginDrawing();
            ClearBackground(Color.LIGHTGRAY);
            DrawBackground();                                                   //Draw the background images
            Scene.DrawRecursive();                                              //Recursively draw all sceneobjects
            DrawHealthBars();                                                   //Draw the healthbars
            //Scene.DrawDebugRecursive();                                       //Draws boxes for all boundingboxes, and lines for all bearings
            EndDrawing();
        }

        //Draws the background
        public void DrawBackground()
        {
            DrawTextureQuad(background, new Vector2(1, 1), Vector2.Zero, view, Color.WHITE);         //Draws the grass
            DrawTextureQuad(overlay, new Vector2(1, 1), Vector2.Zero, view, Color.WHITE);            //Draws the (alpha changed) dirt
        }
    }
}
