using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicalTest.GlobalVariables;
using MFG = MathClasses;
using Raylib;
using static GraphicalTest.GlobalVariables;

namespace GraphicalTest
{
    class Bullet : SceneObject
    {
        private static float speed = 20;

        public Bullet(MFG.Vector3 position, float rotation, SpriteSet sprites) : base(position, DistDirToXY(speed, rotation), rotation, sprites, Scene)
        {
            MaxSpeed = 50F;
            image = sprites.images[0];
        }
    }
}
