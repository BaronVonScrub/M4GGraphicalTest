using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using System.IO;

namespace GraphicalTest
{
    static class Sprites
    {
        internal static Texture2D TANK_BEIGE = LoadTextureFromImage(LoadImage("../Images/Tanks/tankBeige_outline.png"));
        internal static Texture2D TANK_BLACK = LoadTextureFromImage(LoadImage("../Images/Tanks/tankBlack_outline.png"));
        internal static Texture2D TANK_BLUE = LoadTextureFromImage(LoadImage("../Images/Tanks/tankBlue_outline.png"));
        internal static Texture2D TANK_GREEN = LoadTextureFromImage(LoadImage("../Images/Tanks/tankGreen_outline.png"));
        internal static Texture2D TANK_RED = LoadTextureFromImage(LoadImage("../Images/Tanks/tankRed_outline.png"));

        internal static Texture2D BARREL_BEIGE = LoadTextureFromImage(LoadImage("../Images/Tanks/barrelBeige_outline.png"));
        internal static Texture2D BARREL_BLACK = LoadTextureFromImage(LoadImage("../Images/Tanks/barrelBlack_outline.png"));
        internal static Texture2D BARREL_BLUE = LoadTextureFromImage(LoadImage("../Images/Tanks/barrelBlue_outline.png"));
        internal static Texture2D BARREL_GREEN = LoadTextureFromImage(LoadImage("../Images/Tanks/barrelGreen_outline.png"));
        internal static Texture2D BARREL_RED = LoadTextureFromImage(LoadImage("../Images/Tanks/barrelRed_outline.png"));
    }
}
