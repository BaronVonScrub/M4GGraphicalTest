using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace GraphicalTest
{
    static class Sprites
    {
        static string d = GetWorkingDirectory()+"\\";

        internal static Image TANK_BEIGE = LoadImage(d+"tankBeige_outline.png");
        internal static Image TANK_BLACK = LoadImage(d + "tankBlack_outline.png");
        internal static Image TANK_BLUE = LoadImage(d + "tankBlue_outline.png");
        internal static Image TANK_GREEN = LoadImage(d + "tankGreen_outline.png");
        internal static Image TANK_RED = LoadImage(d + "tankRed_outline.png");

        internal static Image BARREL_BEIGE = LoadImage(d + "barrelBeige_outline.png");
        internal static Image BARREL_BLACK = LoadImage(d + "barrelBlack_outline.png");
        internal static Image BARREL_BLUE = LoadImage(d + "barrelBlue_outline.png");
        internal static Image BARREL_GREEN = LoadImage(d + "barrelGreen_outline.png");
        internal static Image BARREL_RED = LoadImage(d + "barrelRed_outline.png");
    }
}
