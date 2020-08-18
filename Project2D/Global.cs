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

        public static List<Collision> Collisions { get; set; } = new List<Collision>();

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

        //Return all valid collisions between objects in a given list
        internal static void CollisionChecks()
        {
            var collisionList = new List<Collision>();                                                          //Prep a list

            for (int i = 0; i < ObjectList.Count; i++)                                                                     //For each object
            {
                SceneObject currObj = ObjectList[i];
                for (int j = i + 1; j < ObjectList.Count; j++)                                                             //For each object beyond that
                {
                    SceneObject otherObj = ObjectList[j];

                    if (currObj.typeIgnore.Contains(otherObj.GetType()) || otherObj.typeIgnore.Contains(currObj.GetType()))                                          //If it is a type ignore,
                        continue;                                                                                         //Skip

                    if (currObj.specificIgnore.Contains(otherObj) || otherObj.specificIgnore.Contains(currObj))                                                //If it is a specific ignore,
                        continue;                                                                                         //Skip

                    if (DistanceBetweenObjs(currObj, otherObj) > currObj.maxBoxDimension + otherObj.maxBoxDimension)    //If they are too far apart to touch,
                        continue;                                                                                       //Skip

                    if (!Collides(currObj.Box, otherObj.Box))                                                          //If oriented bounding boxes don't collide
                        continue;                                                                                     //Skip

                    collisionList.Add(new Collision(currObj, otherObj));                                                //Add pair to final list if all checks passed
                }
            }
            Collisions = collisionList;
        }

        internal static bool Collides(BoundingBox a, BoundingBox b)
        {
            var origin = new MFG.Vector3(0, 0, 1);
            int aLen = a.vertices.Length;
            int bLen = b.vertices.Length;

            for (int i = 0; i < aLen; i++)
            {
                var lineA = new VecLine(a.vertices[i], a.vertices[(i + 1) % aLen]);
                for (int j = 0; j < bLen; j++)
                {
                    var lineB = new VecLine(b.vertices[j], b.vertices[(j + 1) % bLen]);
                    if (Intersects(lineA, lineB))
                    {
                        Console.WriteLine("Line collision: "
                                                            + lineA.p.x.ToString() + "," + lineA.p.y.ToString() + "+" + lineA.v.x.ToString() + "," + lineA.v.y.ToString() + " and " +
                                                            lineB.p.x.ToString() + "," + lineB.p.y.ToString() + "+" + lineB.v.x.ToString() + "," + lineB.v.y.ToString());
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool Intersects(VecLine a, VecLine b)
        {
            #region Pseudonym variables
            float A = a.p.x;
            float B = a.p.y;
            float C = b.p.x;
            float D = b.p.y;
            float E = a.v.x;
            float F = a.v.y;
            float G = b.v.x;
            float H = b.v.y;
            #endregion

            #region Derivation maths
            //Parametric line = line.point + w*line.vector, 0 <= w <= 1
            //So,
            /*
            lineA = a.p+s*a.v
            lineB = b.p+t*b.v

            lineA.x = A+s*E
            lineB.x = C+t*G

            lineA.y = B+s*F
            lineB.y = D+t*H

            lineA.x=lineB.x
            A+s*E=C+t*G
            s=(C+t*G-A)/E

            lineA.y=lineB.y
            B+s*F=D+t*H
            t=(B+s*F-D)/H

            Substitution
            t=(B+((C+t*G-A)/E)*F-D)/H
            t=(F(C-A)+E(B-D))/(EH-FG)

            s=(C+t*G-A)/E
            */
            #endregion
            #region Parallel check pseudocode
            //if ) a.v.Dot(b.v) == a.v.Magnitude() * b.v.Magnitude() || a.v.Dot(b.v) == -a.v.Magnitude() * b.v.Magnitude()))
            //parallel or antiparallel; will not cross unless collinear.Add check for this
            #endregion
            if ((E * H == 0) || (F * G - E * H < 0.000001F))    //EDGE CASE DETECTED, Parallel or AntiParallel (?)
                #region Derivation maths
                //I think this handles the above dot product check.
                //ADD CHECK FOR COLLINEARITY
                //If some w exists such that a.p + w*a.v = b.p, they are collinear.
                //So if w exists such that a.p.x+w*a.v.x = b.p.x AND a.p.y+w*a.v.y = b.p.y, then they are collinear.
                //a.p.x+w*a.v.x = b.p.x
                //w = (b.p.x - a.p.x)/a.v.x
                //w = (b.p.y. - a.p.y)/a.v.y
                //Then (b.p.x - a.p.x)/a.v.x = (b.p.y. - a.p.y)/a.v.y
                //Then a.v.y*(b.p.x-a.p.x) = a.v.x*(b.p.y. - a.p.y)
                //(F(C-A) == E(D-B))
                #endregion
                if (F*(C-A) != E*(D-B))                         //If they are not collinear, as determined above
                    return false;                               //Return false, as they will never meet.


            //From above
            float t = (F*(C - A) + E*(B - D)) / (E*H - F*G);

            //From above
            float s = (C + t * G - A) / E;

            //(s,t) is the point of intersection; if they both exist within the range 0,1, then the lines intersect
            return ((s >= 0 && s <= 1) && (t >= 0 && t <= 1));
        }

        internal static void CollisionProcess()
        {
            foreach (Collision coll in Collisions)
            {
                Console.WriteLine("COLLISION! " + LastTime.ToString());
            }
        }

        internal struct VecLine
        {
            internal MFG.Vector3 p;
            internal MFG.Vector3 v;

            internal VecLine(MFG.Vector3 startPoint, MFG.Vector3 endPoint)
            {
                p = startPoint;
                v = endPoint - startPoint;
            }
        }

        internal struct Collision
        {
            internal SceneObject a;
            internal SceneObject b;

            public Collision(SceneObject a, SceneObject b)
            {
                this.a = a;
                this.b = b;
            }
        }


        internal static float DistanceBetweenObjs(SceneObject a, SceneObject b)
        {
            float xx = Math.Abs(a.GlobalPosition.x - b.GlobalPosition.x);
            float yy = Math.Abs(a.GlobalPosition.y - b.GlobalPosition.y);
            return (float)Math.Sqrt(xx * xx + yy * yy);

        }

        internal static void Log(float val) => Console.WriteLine(val);

        internal static void Log(Bullet val) => Console.WriteLine(val.ToString());

        internal static void Log(MFG.Vector3 val) => Console.WriteLine("{" + val.x + "," + val.y + "," + val.z + "}");
    }

    internal struct TankState
    {
        private readonly MFG.Vector3 position, velocity;
        private readonly Turret turret;

        public TankState(MFG.Vector3 position, MFG.Vector3 velocity, Turret turret)
        {
            this.position = position;
            this.velocity = velocity;
            this.turret = turret;
        }
    }

    internal struct TurretState
    {
        private readonly MFG.Vector3 relativePosition;
        private readonly float aimDirection;

        public TurretState(MFG.Vector3 relativePosition, float aimDirection)
        {
            this.relativePosition = relativePosition;
            this.aimDirection = aimDirection;
        }
    }

    internal struct BulletState
    {
        private readonly MFG.Vector3 position, velocity;

        public BulletState(MFG.Vector3 position, MFG.Vector3 velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }
    }

    internal struct SpriteSet
    {
        internal Texture2D[] images;

        public SpriteSet(Image[] images)
        {
            var temp = new Texture2D[images.Length];
            for (int i = 0; i < images.Length; i++)
                temp[i] = LoadTextureFromImage(images[i]);
            this.images = temp;
        }
    }

    internal struct BoundingBox
    {
        internal MFG.Vector3[] vertices;

        internal BoundingBox(MFG.Vector3[] vertices) => this.vertices = vertices;
    }
}
