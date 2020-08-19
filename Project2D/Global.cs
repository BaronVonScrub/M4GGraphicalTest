using Raylib;
using System;
using System.Collections.Generic;
using static Raylib.Raylib;
using MFG = MathClasses;

namespace GraphicalTest
{
    internal static class GlobalVariables
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

        internal static float LastTime { get; set; }

        public static SceneObject Scene { get; set; } = new SceneObject();

        public static List<SceneObject> ObjectList { get; set; } = new List<SceneObject>();

        public static readonly int WindowWidth = 1600;
        public static readonly int WindowHeight = 950;

        internal static MFG.Vector3 DistDirToXY(float distance, float direction) => new MFG.Vector3(distance * (float)-Math.Sin(direction), distance * (float)Math.Cos(direction), 0);

        internal static Vector2 DistDirToXYV2(float distance, float direction) => new Vector2(distance * (float)-Math.Sin(direction), distance * (float)Math.Cos(direction));

        internal static MFG.Vector3 PointOffsetDistDir(float distance, float direction) => new MFG.Vector3(distance * (float)Math.Sin(direction), distance * (float)Math.Cos(direction), 1);

        internal static Vector2 ConvertV3ToV2(MFG.Vector3 vec) => new Vector2(vec.x, vec.y);

        public static MFG.Matrix3 RotationMatrix2D(float v) => new MFG.Matrix3((float)Math.Cos(-v), (float)-Math.Sin(-v), 0,
                                      (float)Math.Sin(-v), (float)Math.Cos(-v), 0,
                                      0, 0, 1);

        internal static float CalcBoxSize(MFG.Vector3 offset, Texture2D image)
        {
            float xx = Math.Max(Math.Abs(offset.x), Math.Abs(image.width - offset.x));
            float yy = Math.Max(Math.Abs(offset.y), Math.Abs(image.height - offset.y));
            return (float)Math.Sqrt(xx * xx + yy * yy);
        }

        public static MFG.Matrix3 RotationMatrix2D(float rotation, Raylib.Vector3 pivot)
        {
            float r = rotation;
            Raylib.Vector3 p = pivot;

            return new MFG.Matrix3(
                (float)Math.Cos(r), (float)Math.Sin(r), 0,
                (float)-Math.Sin(r), (float)Math.Cos(r), 0,
                -p.x * (float)Math.Cos(r) + p.y * (float)Math.Sin(r) + p.x, -p.x * (float)Math.Sin(r) - p.y * (float)Math.Cos(r) + p.y, 1);
        }
    }
}
