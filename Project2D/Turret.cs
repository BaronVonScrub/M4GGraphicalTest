using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicalTest.GlobalVariables;
using MFG = MathClasses;
using static Raylib.Raylib;
using Raylib;


namespace GraphicalTest
{
    class Turret : SceneObject
    {
        private static float length=10;

        private MFG.Vector3 aimPosition;
        float rotation;
        internal Image sprite;
        internal Image bulletSprite;

        public new float Rotation { get => rotation;
            set
            {
                if (value == rotation)
                    return;
                rotation = (value + 2 * (float)Math.PI) % (2 * (float)Math.PI);
                aimPosition = DistDirToXY(length,rotation);
                MakeDirty();
            }
        }

        public Turret(MFG.Vector3 position, float rotation, SpriteSet sprites, SceneObject parent)
            : base(position, new MFG.Vector3(0, 0, 0), rotation, sprites, parent)
        {
            MaxSpeed = 0F;
            image = sprites.images[0];
        }

        internal Bullet Fire() => new Bullet(aimPosition,rotation,sprites);
    }
}
