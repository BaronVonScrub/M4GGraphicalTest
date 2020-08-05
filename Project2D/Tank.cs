using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicalTest.Globals;

namespace GraphicalTest
{
    class Tank
    {
        static float MaxSpeed = 5F;
        static float MinSpeed = -2F;
        static float Acceleration = 0.1F;
        static float Deceleration = 0.05F;
        static float TurnSpeed = 3F;
        static float TurretSpeed = 3F;

        float x, y, dir, vel;
        Turret turret;
        List<Bullet> bullets = new List<Bullet>();

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float Dir { get => dir; set => dir = value; }
        public float Speed { get => vel; set => vel = value; }

        public Tank(float x, float y, float rot, float turretRot)
        {
            this.X = x;
            this.Y = y;
            this.Dir = rot;
            turret = new Turret(0,0,0);
        }

        internal float Forward() => Speed = Globals.Clamp<float>(Speed+Acceleration, MinSpeed, MaxSpeed);
        internal float Backward() => Speed = Globals.Clamp<float>(Speed-Deceleration, MinSpeed, MaxSpeed);

        internal float TurnLeft() => Dir = (float)((Dir + TurnSpeed) % (2 * Math.PI));
        internal float TurnRight() => Dir = (float)((Dir - TurnSpeed) % (2*Math.PI));

        internal float TurretLeft() => turret.Dir = (float)((turret.Dir + TurretSpeed) % (2 * Math.PI));
        internal float TurretRight() => turret.Dir = (float)((turret.Dir - TurretSpeed) % (2 * Math.PI));

        internal Coordinate UpdatePosition()
        {
            X += Speed * (float)Math.Cos(Dir);
            Y += Speed * (float)Math.Sin(Dir);
            return new Coordinate(X, Y);
        }
    }
}
