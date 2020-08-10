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

        MFG.Vector3 relativePosition;
        private MFG.Vector3 aimPosition;
        float aimDirection;
        internal Image sprite;
        internal Image bulletSprite;

        public float AimDirection { get => aimDirection;
            set
            {
                aimDirection = (value + 2*(float)Math.PI) % (2*(float)Math.PI);
                aimPosition = DistDirToXY(length,aimDirection);
            }
        }

        public Turret(MFG.Vector3 relativePosition, float aimDirection, TurretSpriteSet sprites)
        {
            this.relativePosition = relativePosition;
            this.aimDirection = aimDirection;
            this.sprite = sprites.barrel;
            this.bulletSprite = sprites.bullet;
            allTurrets.Add(this);
        }


        public TurretState Update()
        {
            return new TurretState(relativePosition,aimDirection);
        }

        internal Bullet Fire() => new Bullet(aimPosition,aimDirection,bulletSprite);

        internal void Draw()
        {
            
        }
    }
}
