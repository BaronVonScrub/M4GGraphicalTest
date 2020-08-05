using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        internal static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        internal static void Log(float val)
        {
            Console.WriteLine(val);
        }

        internal static void Log(Coordinate val)
        {
            Console.WriteLine("{" + val.x + "," + val.y + "}");
        }

        internal static void Log(Bullet val)
        {
            Console.WriteLine(val.ToString());
        }
    }

    struct TankState
    {
        float x, y, dir, speed;
        Turret turret;

        public TankState(float x, float y, float dir, float speed, Turret turret)
        {
            this.x = x;
            this.y = y;
            this.dir = dir;
            this.speed = speed;
            this.turret = turret;
        }
    }

    struct TurretState
    {
        float relX, relY, dir;

        public TurretState(float relX, float relY, float dir)
        {
            this.relX = relX;
            this.relY = relY;
            this.dir = dir;
        }
    }

    struct BulletState
    {
        float x, y, dir;

        public BulletState(float x, float y, float dir)
        {
            this.x = x;
            this.y = y;
            this.dir = dir;
        }
    }

    struct Coordinate
    {
        internal float x, y;
        internal Coordinate(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
