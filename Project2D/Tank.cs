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
        static float MaxSpeed = 50F;
        static float AccRate = 30F;
        static float DecRate = 15F;
        static float TurnSpeed = 1F;
        static float TurretSpeed = 1F;

        Vector3 position;
        Vector3 velocity;
        Vector3 acceleration;

        internal Turret turret;
        TankSpriteSet sprites;
        Image body;
        float pointDirection;

        public Tank(Vector3 position, Vector3 velocity, float rotation, float turretRot, TankSpriteSet sprites)
        {
            this.position = position;
            this.Velocity = velocity;
            this.body = sprites.body;
            turret = new Turret(new Vector3(0,0,0),0,sprites.turretSet);

            allTanks.Add(this);

            this.Rotation = rotation;
        }

        internal Vector3 Forward() => acceleration = DistDirToXY(AccRate, Rotation);
        internal Vector3 Backward() => acceleration = DistDirToXY(DecRate, Rotation+(float)Math.PI);

        internal float TurnLeft() => RotationShift = TurnSpeed;
        internal float TurnRight() => RotationShift = -TurnSpeed;

        internal float TurretLeft() => turret.RotationShift = TurretSpeed;
        internal float TurretRight() => turret.RotationShift = -TurretSpeed;

        internal Bullet Fire() => turret.Fire();
    }
}
