﻿using Raylib;
using static Raylib.Raylib;

namespace GraphicalTest
{
    class PlayerController
    {
        //The playercontroller has an associated tank
        Tank player;

        //Constructor assigns a tank
        public PlayerController(Tank player)
        {
            this.player = player;
        }

        //Processes player input, converts it to tank commands
        public void ProcessInput()
        {
            if (IsKeyDown(KeyboardKey.KEY_W) && !IsKeyDown(KeyboardKey.KEY_S)) player.Forward();

            if (IsKeyDown(KeyboardKey.KEY_S) && !IsKeyDown(KeyboardKey.KEY_W)) player.Backward();

            if (IsKeyDown(KeyboardKey.KEY_A) && !IsKeyDown(KeyboardKey.KEY_D)) player.TurnLeft();

            if (IsKeyDown(KeyboardKey.KEY_D) && !IsKeyDown(KeyboardKey.KEY_A)) player.TurnRight();

            if (IsKeyDown(KeyboardKey.KEY_Q) && !IsKeyDown(KeyboardKey.KEY_E)) player.TurretLeft();

            if (IsKeyDown(KeyboardKey.KEY_E) && !IsKeyDown(KeyboardKey.KEY_Q)) player.TurretRight();

            if (IsKeyDown(KeyboardKey.KEY_SPACE)) player.Fire();
        }
    }
}
