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
            return new MFG.Vector3(distance*(float)-Math.Sin(direction), distance * (float)Math.Cos(direction),0);
        }

        internal static Vector2 DistDirToXYV2(float distance, float direction)
        {
            return new Vector2(distance * (float)-Math.Sin(direction), distance * (float)Math.Cos(direction));
        }

        internal static MFG.Vector3 PointOffsetDistDir(float distance, float direction)
        {
            return new MFG.Vector3(distance * (float)Math.Sin(direction), distance * (float)Math.Cos(direction), 1);
        }

        internal static Vector2 ConvertV3ToV2(MFG.Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }

        public static MFG.Matrix3 RotationMatrix2D(float v)
        {
            return new MFG.Matrix3((float)Math.Cos(-v), (float)-Math.Sin(-v), 0,
                                      (float)Math.Sin(-v), (float)Math.Cos(-v), 0,
                                      0, 0, 1);
        }

        public static MFG.Matrix3 RotationMatrix2D(float rotation, Vector3 pivot)
        {
            float r = rotation;
            Vector3 p = pivot;

            return new MFG.Matrix3(
                (float)Math.Cos(r), (float)Math.Sin(r), 0,
                (float)-Math.Sin(r), (float)Math.Cos(r), 0,
                -p.x * (float)Math.Cos(r) + p.y * (float)Math.Sin(r) + p.x, -p.x * (float)Math.Sin(r) - p.y * (float)Math.Cos(r) + p.y, 1);
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
