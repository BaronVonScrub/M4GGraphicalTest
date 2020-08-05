using Raylib;
using static Raylib.Raylib;
using System;
using static GraphicalTest.Globals;

namespace GraphicalTest
{
    class PlayerController
    {
        Tank player;
        public PlayerController(Tank player)
        {
            this.player = player;
        }

        public void UpdateStatus()
        {
            if (IsKeyDown(KeyboardKey.KEY_W) && !IsKeyDown(KeyboardKey.KEY_S))
            {
                Log(player.Forward());
            }

            if (IsKeyDown(KeyboardKey.KEY_S) && !IsKeyDown(KeyboardKey.KEY_W))
            {
                Log(player.Backward());
            }

            if (IsKeyDown(KeyboardKey.KEY_A) && !IsKeyDown(KeyboardKey.KEY_D))
            {
                Log(player.TurnLeft());
            }

            if (IsKeyDown(KeyboardKey.KEY_D) && !IsKeyDown(KeyboardKey.KEY_A))
            {
                Log(player.TurnRight());
            }

            if (IsKeyDown(KeyboardKey.KEY_Q) && !IsKeyDown(KeyboardKey.KEY_E))
            {
                Log(player.TurretLeft());
            }

            if (IsKeyDown(KeyboardKey.KEY_E) && !IsKeyDown(KeyboardKey.KEY_Q))
            {
                Log(player.TurretRight());
            }
        }
    }
}
