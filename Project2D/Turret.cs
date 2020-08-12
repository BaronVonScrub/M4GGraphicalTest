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

        private Vector3 aimPosition;
        float rotation;
        internal Image sprite;
        internal Image bulletSprite;

        public float Rotation { get => rotation;
            set
            {
                rotation = (value + 2*(float)Math.PI) % (2*(float)Math.PI);
                aimPosition = DistDirToXY(length,rotation);
            }
        }

        public Turret(Vector3 relativePosition, float aimDirection, TurretSpriteSet sprites)
        {
            this.relativePosition = relativePosition;
            this.rotation = aimDirection;
            this.sprite = sprites.barrel;
            this.bulletSprite = sprites.bullet;
            allTurrets.Add(this);
        }

        internal Bullet Fire() => new Bullet(aimPosition,rotation,bulletSprite);
    }
}
