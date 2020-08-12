using Raylib;
using static Raylib.Raylib;
using System;
using static GraphicalTest.GlobalVariables;

namespace GraphicalTest
{
    class PlayerController
    {
        Tank player;
        public PlayerController(Tank player)
        {
            this.player = player;
        }

        public void ProcessInput()
        {
            if (IsKeyDown(KeyboardKey.KEY_W) && !IsKeyDown(KeyboardKey.KEY_S))      player.Forward();

            if (IsKeyDown(KeyboardKey.KEY_S) && !IsKeyDown(KeyboardKey.KEY_W))      player.Backward();

            if (IsKeyDown(KeyboardKey.KEY_A) && !IsKeyDown(KeyboardKey.KEY_D))      player.TurnLeft();

            if (IsKeyDown(KeyboardKey.KEY_D) && !IsKeyDown(KeyboardKey.KEY_A))      player.TurnRight();

            if (IsKeyDown(KeyboardKey.KEY_Q) && !IsKeyDown(KeyboardKey.KEY_E))      player.TurretLeft();

            if (IsKeyDown(KeyboardKey.KEY_E) && !IsKeyDown(KeyboardKey.KEY_Q))      player.TurretRight();
            
            if (IsKeyDown(KeyboardKey.KEY_SPACE))                                   player.Fire();
        }
    }
}
