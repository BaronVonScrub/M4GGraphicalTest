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
    static class GlobalVariables
    {
        internal enum SPRITE
        {
            BEIGE,
            BLACK,
            BLUE,
            GREEN,
            RED
        }

        private static float deltaTime;

        internal static float DeltaTime { get => deltaTime; set => deltaTime = value; }

        public static SceneObject Scene { get; set; } = new SceneObject();

        internal static MFG.Vector3 DistDirToXY(float distance, float direction)
        {
            return new MFG.Vector3(distance*(float)Math.Sin(direction), distance * (float)Math.Cos(direction),0);
        }

        internal static MFG.Vector3 PointOffsetDistDir(float distance, float direction)
        {
            return new MFG.Vector3(distance * (float)Math.Sin(direction), distance * (float)Math.Cos(direction), 1);
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

    struct SpriteSet
    {
        internal Texture2D[] images;

        public SpriteSet(Image[] images)
        {
            Texture2D[] temp = new Texture2D[images.Length];
            for (int i = 0; i < images.Length; i++)
                temp[i] = LoadTextureFromImage(images[i]);
            this.images = temp;
        }
    }
}
