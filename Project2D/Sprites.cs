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
        internal static TankSpriteSet TANK_BEIGE =
            new TankSpriteSet(LoadImage("../Images/Tanks/tankBeige_outline.png"),
                              LoadImage("../Images/Tanks/barrelBeige_outline.png"),
                              LoadImage("../Images/Bullets/bulletBeigeSilver_outline.png"));

        internal static TankSpriteSet TANK_BLACK =
            new TankSpriteSet(LoadImage("../Images/Tanks/tankBlack_outline.png"),
                              LoadImage("../Images/Tanks/barrelBlack_outline.png"),
                              LoadImage("../Images/Bullets/bulletSilverSilver_outline.png"));

        internal static TankSpriteSet TANK_BLUE =
            new TankSpriteSet(LoadImage("../Images/Tanks/tankBlue_outline.png"),
                              LoadImage("../Images/Tanks/barrelBlue_outline.png"),
                              LoadImage("../Images/Bullets/bulletBlueSilver_outline.png"));

        internal static TankSpriteSet TANK_GREEN =
            new TankSpriteSet(LoadImage("../Images/Tanks/tankGreen_outline.png"),
                              LoadImage("../Images/Tanks/barrelGreen_outline.png"),
                              LoadImage("../Images/Bullets/bulletGreenSilver_outline.png"));

        internal static TankSpriteSet TANK_RED =
            new TankSpriteSet(LoadImage("../Images/Tanks/tankRed_outline.png"),
                              LoadImage("../Images/Tanks/barrelRed_outline.png"),
                              LoadImage("../Images/Bullets/bulletRedSilver_outline.png"));
    }
}
