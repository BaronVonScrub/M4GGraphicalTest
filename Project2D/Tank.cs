using System;
using System.Collections.Generic;
using static GraphicalTest.Game;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicalTest.Global;
using System.IO;
using Raylib;
using static Raylib.Raylib;
using MFG = MathClasses;

namespace GraphicalTest
{
    class Tank
    {
        static float MaxSpeed = 50F;
        static float AccRate = 30F;
        static float DecRate = 15F;
        static float TurnSpeed = 30F;
        static float TurretSpeed = 30F;

        MFG.Vector3 position;
        MFG.Vector3 velocity;
        internal Turret turret;
        Texture2D sprite;
        float pointDirection;

        public MFG.Vector3 Velocity
        {
            get => velocity;

            set
            {
                velocity = value;
                if (velocity.Magnitude() > MaxSpeed)
                    velocity = velocity * (MaxSpeed / velocity.Magnitude());
            }
        }

        public float PointDirection
        {
            get => pointDirection;
            set
            {
                pointDirection = (value + 360) % 360;
            }
        }

        public Tank(MFG.Vector3 position, MFG.Vector3 velocity, float pointDirection, float turretRot, Texture2D sprite)
        {
            this.position = position;
            this.Velocity = velocity;
            turret = new Turret(new MFG.Vector3(0,0,0),0);
            allTanks.Add(this);
            this.sprite = sprite;
            this.PointDirection = pointDirection;
        }

        internal MFG.Vector3 Forward() => Velocity += DeltaTime * DistDirToXY(AccRate, PointDirection);
        internal MFG.Vector3 Backward() => Velocity += DeltaTime * DistDirToXY(DecRate, PointDirection+(float)Math.PI);

        internal float TurnLeft() => PointDirection += DeltaTime * TurnSpeed;
        internal float TurnRight() => PointDirection -= DeltaTime * TurnSpeed;

        internal float TurretLeft() => turret.AimDirection += DeltaTime * TurretSpeed;
        internal float TurretRight() => turret.AimDirection -= DeltaTime * TurretSpeed;
        internal Bullet Fire() => turret.Fire();

        internal TankState Update()
        {
            position += velocity * DeltaTime;
            return new TankState(position,Velocity,turret);
        }

        internal void Draw()
        {
            DrawTexture(sprite,(int)position.x,(int)position.y,Color.WHITE);
        }
    }
}
