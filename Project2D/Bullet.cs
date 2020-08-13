using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicalTest.GlobalVariables;
using MFG = MathClasses;
using Raylib;
namespace GraphicalTest
{
    class Bullet : SceneObject
    {
        private static float speed = 1000;

        public Bullet(MFG.Vector3 position, float rotation, SpriteSet sprites) : base(position, DistDirToXY(speed, rotation), rotation+(float)Math.PI, sprites, Scene)
        {
            image = sprites.images[2];
            friction = 0;
            offset = new MFG.Vector3(-image.width / 2, -image.height / 2, 0);
            scale = 0.8F;
        }
    }
}
