using Raylib;
using System;
using System.Collections.Generic;
using static Raylib.Raylib;
using MFG = MathClasses;

namespace GraphicalTest
{
    internal static class Global
    {
        //Publicly accessible deltatime
        internal static float DeltaTime { get; set; }

        //Publicly accessible master SceneObject
        public static SceneObject Scene { get; set; } = new SceneObject();

        //Publicly accessible list of all objects
        public static List<SceneObject> ObjectList { get; set; } = new List<SceneObject>();

        //Stores game window width and height
        public static readonly int WindowWidth = 1600;
        public static readonly int WindowHeight = 950;

        //Returns a vector formed of a distance in a direction
        internal static MFG.Vector3 DistDirToXY(float distance, float direction) => new MFG.Vector3(distance * (float)-Math.Sin(direction), distance * (float)Math.Cos(direction), 0);

        //Returns a Vector2 formed of a distance in a direction
        internal static Vector2 DistDirToXYV2(float distance, float direction) => new Vector2(distance * (float)-Math.Sin(direction), distance * (float)Math.Cos(direction));

        //Returns a point offset by a distance in a direction
        internal static MFG.Vector3 PointOffsetDistDir(float distance, float direction) => new MFG.Vector3(distance * (float)Math.Sin(direction), distance * (float)Math.Cos(direction), 1);

        //Converts a homogenous V3 to a V2
        internal static Vector2 ConvertV3ToV2(MFG.Vector3 vec) => new Vector2(vec.x, vec.y);

        //Returns the 2D rotation matrix of a given angle
        public static MFG.Matrix3 RotationMatrix2D(float v) => new MFG.Matrix3((float)Math.Cos(-v), (float)-Math.Sin(-v), 0,
                                      (float)Math.Sin(-v), (float)Math.Cos(-v), 0,
                                      0, 0, 1);

        //Calculates the maximum boundingbox size of a standard rectangular boundingbox
        internal static float CalcBoxSize(MFG.Vector3 offset, Texture2D image)
        {
            float xx = Math.Max(Math.Abs(offset.x), Math.Abs(image.width - offset.x));
            float yy = Math.Max(Math.Abs(offset.y), Math.Abs(image.height - offset.y));
            return (float)Math.Sqrt(xx * xx + yy * yy);
        }

        //Draws all healthbars (hard coded to be only used by tanks at this point)
        internal static void DrawHealthBars()
        {
            foreach (SceneObject obj in ObjectList)
            {
                if (obj.GetType() != typeof(Tank))
                    continue;

                (obj as Tank).DrawHealthBar();
            }
        }
    }
}
