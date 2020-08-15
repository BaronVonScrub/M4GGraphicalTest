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

        public static List<SceneObject> ObjectList { get; set; } = new List<SceneObject>();

        public static List<Collision> Collisions { get; set; } = new List<Collision>();

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

        //Return all valid collisions between objects in a given list
        internal static void CollisionChecks()
        {
            List<Collision> collisionList = new List<Collision>();                                                          //Prep a list

                for (int i = 0; i < ObjectList.Count; i++)                                                                     //For each object
                {
                    SceneObject currObj = ObjectList[i];
                    for (int j = i + 1; j < ObjectList.Count; j++)                                                             //For each object beyond that
                    {
                        SceneObject otherObj = ObjectList[j];

                        if (currObj.typeIgnore.Contains(otherObj.GetType()))                                          //If it is a type ignore,
                        continue;                                                                                         //Skip

                        if (currObj.specificIgnore.Contains(otherObj))                                                //If it is a specific ignore,
                        continue;                                                                                         //Skip

                        if (DistanceBetweenObjs(currObj, otherObj) > currObj.maxBoxDimension + otherObj.maxBoxDimension)    //If they are too far apart to touch,
                            continue;                                                                                       //Skip

                    if (!Collides(currObj.Box, otherObj.Box))                                                          //If oriented bounding boxes don't collide
                        continue;                                                                                     //Skip

                        collisionList.Add(new Collision(currObj, otherObj));                                                //Add pair to final list if all checks passed
                    }
                }
            Collisions =  collisionList;
        }

        internal static Boolean Collides(BoundingBox a, BoundingBox b)
        {
            MFG.Vector3 origin = new MFG.Vector3(0, 0, 1);
            int aLen = a.vertices.Length;
            int bLen = b.vertices.Length;

            for (int i = 0; i < aLen; i++)
            {
                VecLine lineA = new VecLine(a.vertices[i],a.vertices[(i + 1) % aLen]);
                for (int j = 0; j < bLen; j++)
                {
                    VecLine lineB = new VecLine(b.vertices[j], b.vertices[(j + 1) % bLen]);
                    if (Intersects(lineA, lineB))
                        return true;
                }
            }
            return false;
        }

        private static bool Intersects(VecLine a, VecLine b)
        {
            #region Derivation maths
            //Parametric line = line.point + w*line.vector, 0 <= w <= 1
            //So,
            //Parametric lineA = a.p+s*a.v, 0 <= s <= 1
            //Parametric lineB = b.p+t*b.v, 0 <= t <= 1

            //lineA x = a.p.x+s*a.v.x;
            //lineB x = b.p.x+t*b.v.x;
            //lineA y = a.p.y+s*a.v.y;
            //lineB y = b.p.y+t*b.v.y;

            //The line segments intersect where a.x = b.x and a.y = b.y

            //lineA x = lineB x
            //a.p.x+s*a.v.x = b.p.x+t*b.v.x
            //s=(b.p.x+t*b.v.x - a.p.x)/a.v.x

            //lineA y = lineB y
            //a.p.y+s*a.v.y = b.p.y+t*b.v.y
            //s=(b.p.y+t*b.v.y-a.p.y)/a.v.y

            //(b.p.x+t*b.v.x - a.p.x)/a.v.x = (b.p.y+t*b.v.y-a.p.y)/a.v.y
            //a.v.y*(b.p.x + t*b.v.x - a.p.x) = a.v.x*(b.p.y+t*b.v.y-a.p.y)
            //a.v.y*(b.p.x - a.p.x) = a.v.x*(b.p.y+t*b.v.y-a.p.y) - a.v.y*t*b.v.x
            //a.v.y*(b.p.x - a.p.x) = a.v.x*(b.p.y-a.p.y) - a.v.y*t*b.v.x + a.v.x*t*b.v.y
            //a.v.x*(b.p.y-a.p.y) - a.v.y*(b.p.x - a.p.x) = a.v.y*t*b.v.x + a.v.x*t*b.v.y
            //a.v.x*(b.p.y-a.p.y) - a.v.y*(b.p.x - a.p.x) = t*(a.v.y*b.v.x + a.v.x*b.v.y)
            #endregion

            //t=(a.v.x*(b.p.y-a.p.y) - a.v.y*(b.p.x - a.p.x))/(a.v.y*b.v.x + a.v.x*b.v.y)
            float t = (a.moveVec.x * (b.startPoint.y - a.startPoint.y) - a.moveVec.y * (b.startPoint.x - a.startPoint.x))/(a.moveVec.y*b.moveVec.x + a.moveVec.x*b.moveVec.y);

            //From above
            //s=(b.p.x+t*b.v.x - a.p.x)/a.v.x
            float s = (b.startPoint.x+t*b.moveVec.x - a.startPoint.x)/a.moveVec.x;

            //(s,t) is the point of intersection; if they both exist within the range 0,1, then the lines intersect
            return ((s >= 0 && s <= 1) && (t >= 0 && t <= 1));
        }

        private static Boolean SameSlope(VecLine a, VecLine b)
        {
            return (a.moveVec.x / b.moveVec.x == a.moveVec.y / b.moveVec.y);
        }

        internal struct VecLine
        {
            internal MFG.Vector3 startPoint;
            internal MFG.Vector3 moveVec;

            internal VecLine(MFG.Vector3 startPoint, MFG.Vector3 endPoint)
            {
                this.startPoint = startPoint;
                this.moveVec = endPoint - startPoint;
            }
        }

        internal struct Collision
        {
            SceneObject a;
            SceneObject b;

            public Collision(SceneObject a, SceneObject b)
            {
                this.a = a;
                this.b = b;
            }
        }


        internal static float DistanceBetweenObjs(SceneObject a, SceneObject b)
        {
            var xx = Math.Abs(a.GlobalPosition.x - b.GlobalPosition.x);
            var yy = Math.Abs(a.GlobalPosition.y - b.GlobalPosition.y);
            return (float)Math.Sqrt(xx * xx + yy * yy);

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

    internal struct BoundingBox
    {
        internal MFG.Vector3[] vertices;

        internal BoundingBox(MFG.Vector3[] vertices)
        {
            this.vertices = vertices;
        }
    }
}
