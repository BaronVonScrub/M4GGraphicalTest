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

        public Bullet(MFG.Vector3 position, float rotation, SpriteSet sprites, List<SceneObject> ignoreList, SceneObject ignoreParent) : base(position, DistDirToXY(speed, rotation), rotation+(float)Math.PI, sprites, Scene)
        {
            image = sprites.images[2];
            friction = 0;
            offset = new MFG.Vector3(-image.width / 2, -image.height / 2, 0);
            scale = 1F;
            maxBoxDimension = CalcBoxSize(offset, image);
            Box = GetDefaultBoundingBox();
            specificIgnore.AddRange(ignoreList.FindAll(x=>(x!=Scene)));
            specificIgnore.Add(ignoreParent);
        }
    }
}
 