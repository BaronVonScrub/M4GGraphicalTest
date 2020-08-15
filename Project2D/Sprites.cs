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

        internal static SpriteSet TANK_BEIGE =
            new SpriteSet(new Image[]{LoadImage("../Images/Tanks/tankBeige_outline.png"),
                              LoadImage("../Images/Tanks/barrelBeige_outline.png"),
                              LoadImage("../Images/Bullets/bulletBeigeSilver_outline.png") });

        internal static SpriteSet TANK_BLACK =
            new SpriteSet(new Image[]{LoadImage("../Images/Tanks/tankBlack_outline.png"),
                              LoadImage("../Images/Tanks/barrelBlack_outline.png"),
                              LoadImage("../Images/Bullets/bulletSilverSilver_outline.png") });

        internal static SpriteSet TANK_BLUE =
            new SpriteSet(new Image[]{LoadImage("../Images/Tanks/tankBlue_outline.png"),
                              LoadImage("../Images/Tanks/barrelBlue_outline.png"),
                              LoadImage("../Images/Bullets/bulletBlueSilver_outline.png") });

        internal static SpriteSet TANK_GREEN =
            new SpriteSet(new Image[]{LoadImage("../Images/Tanks/tankGreen_outline.png"),
                              LoadImage("../Images/Tanks/barrelGreen_outline.png"),
                              LoadImage("../Images/Bullets/bulletGreenSilver_outline.png") });

        internal static SpriteSet TANK_RED =
            new SpriteSet(new Image[]{LoadImage("../Images/Tanks/tankRed_outline.png"),
                              LoadImage("../Images/Tanks/barrelRed_outline.png"),
                              LoadImage("../Images/Bullets/bulletRedSilver_outline.png") });
    }
}
