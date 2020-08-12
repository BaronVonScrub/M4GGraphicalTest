using System;
using System.Collections.Generic;
using static GraphicalTest.Game;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicalTest.GlobalVariables;
using System.IO;
using Raylib;
using static Raylib.Raylib;
using MFG = MathClasses;

namespace GraphicalTest
{
    class Tank : SceneObject
    {
        static float AccRate = 30F;
        static float DecRate = 15F;
        static float TurnSpeed = 1F;
        static float TurretSpeed = 1F;

        internal Turret turret;

        public Tank(MFG.Vector3 position,  MFG.Vector3 velocity, float rotation, float turretRot, SpriteSet sprites, SceneObject parent)
            : base(position,velocity, rotation, sprites, parent)
        {
            MaxSpeed = 50F;
            image = sprites.images[0];
            turret = new Turret(new  MFG.Vector3(0,0,0),0,sprites, this);
        }

        internal  MFG.Vector3 Forward() => acceleration = DistDirToXY(AccRate, Rotation);
        internal  MFG.Vector3 Backward() => acceleration = DistDirToXY(DecRate, Rotation+(float)Math.PI);

        internal float TurnLeft() => RotationShift = TurnSpeed;
        internal float TurnRight() => RotationShift = -TurnSpeed;

        internal float TurretLeft() => turret.RotationShift = TurretSpeed;
        internal float TurretRight() => turret.RotationShift = -TurretSpeed;

        internal Bullet Fire() => turret.Fire();
    }
}
