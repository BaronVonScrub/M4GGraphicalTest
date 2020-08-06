using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFG = MathClasses;
using Raylib;
using static Raylib.Raylib;

namespace GraphicalTest
{
    static class Global
    {
        internal enum SPRITE
        {
            BEIGE,
            BLACK,
            BLUE,
            GREEN,
            RED
        }

        internal static List<Tank> allTanks = new List<Tank>();
        internal static List<Bullet> allBullets = new List<Bullet>();
        internal static List<Turret> allTurrets = new List<Turret>();

        private static float deltaTime;

        internal static float DeltaTime { get => deltaTime; set => deltaTime = value; }

        internal static MFG.Vector3 DistDirToXY(float distance, float direction)
        {
            return new MFG.Vector3(distance*(float)Math.Sin(direction), distance * (float)Math.Cos(direction));
        }

        internal static void Log(float val)
        {
            Console.WriteLine(val);
        }

        internal static void Log(Bullet val)
        {
            Console.WriteLine(val.ToString());
        }

        internal static void Log(MFG.Vector3 val)
        {
            Console.WriteLine("{"+val.x+","+val.y+","+val.z+"}");
        }
    }

    struct TankState
    {
        MFG.Vector3 position, velocity;
        Turret turret;

        public TankState(MFG.Vector3 position, MFG.Vector3 velocity, Turret turret)
        {
            this.position = position;
            this.velocity = velocity;
            this.turret = turret;
        }
    }

    struct TurretState
    {
        MFG.Vector3 relativePosition;
        float aimDirection;

        public TurretState(MFG.Vector3 relativePosition, float aimDirection)
        {
            this.relativePosition = relativePosition;
            this.aimDirection = aimDirection;
        }
    }

    struct BulletState
    {
        MFG.Vector3 position, velocity;

        public BulletState(MFG.Vector3 position, MFG.Vector3 velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }
    }

    struct TankSpriteSet
    {
        internal Image body;
        internal TurretSpriteSet turretSet;

        public TankSpriteSet(Image body, Image barrel, Image bullet)
        {
            this.body = body;
            turretSet = new TurretSpriteSet(barrel, bullet);
        }
    }

    struct TurretSpriteSet
    {
        internal Image barrel, bullet;

        public TurretSpriteSet(Image barrel, Image bullet)
        {
            this.barrel = barrel;
            this.bullet = bullet;
        }
    }
}
