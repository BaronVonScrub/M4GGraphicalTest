using System;
using System.Collections.Generic;
using System.Linq;
using static GraphicalTest.GlobalVariables;
using System.Text;
using System.Threading.Tasks;
using MFG = MathClasses;
using Raylib;
using static Raylib.Raylib;

namespace GraphicalTest
{
    class CollisionManager
    {

        public static List<Collision> Collisions { get; set; } = new List<Collision>();

        //Return all valid collisions between objects in a given list
        internal static void CollisionChecks()
        {
            var collisionList = new List<Collision>();                                                          //Prep a list

            for (int i = 0; i < ObjectList.Count; i++)                                                                     //For each object
            {
                SceneObject currObj = ObjectList[i];
                {
                    if (currObj.GetType() == typeof(Bullet))
                        if (currObj.GlobalPosition.x < 0 || currObj.GlobalPosition.x > WindowWidth || currObj.GlobalPosition.y < 0 || currObj.GlobalPosition.y > WindowHeight)
                        {
                            collisionList.Add(new Collision(currObj));
                            continue;
                        }
                    
                }
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

        /*
        * Narrow phase collision detection considers each line of the bounding box of object A vs each line in the bounding box of object B.
        * If slopes match+not collinear, it escapes as false.
        * It then checks for collisions by representing each line as a parametric linear equation point+t*vector, then checks for where their (x,y) match.
        * If the lines collide such that both parametric variables are between 0 and 1, there is a collision between the line segments.
        */
        private static bool Collides(BoundingBox a, BoundingBox b)
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
                        return true;
                }
            }
            return false;
        }

        private static bool Intersects(VecLine a, VecLine b)
        {
            //Derived from https://blogs.sas.com/content/iml/2018/07/09/intersection-line-segments.html
            MFG.Vector3 p1 = a.p;
            MFG.Vector3 p2 = a.p + a.v;
            MFG.Vector3 q1 = b.p;
            MFG.Vector3 q2 = b.p + b.v;

            MFG.Matrix3 A = new MFG.Matrix3((p2 - p1).x, (p2 - p1).y, 0, (q1 - q2).x, (q1 - q2).y, 0, 0, 0, 1);

            MFG.Vector3 B = q1 - p1;

            //Then
            //A*t = b
            //t = A^-1 * b

            MFG.Matrix3 inverted = A.GetInverted();

            if (inverted != null)                                                                       //If it is invertible, they don't share a slope. The lines will intersect.
            {
                MFG.Vector3 solution = inverted * B;                                                    //Solve the equation
                return ((solution.x >= 0 && solution.x <= 1) && (solution.y >= 0 && solution.y <= 1));  //If the solution falls in the unit square, valid collision, else not.
            }
            else                                                                                        //Otherwise, if they share a slope,
            {                                                                                           //Check for overlaps
                if (IsBetween(p1, p2, q1))                                                                //Is q1 between p1 and p2?
                    return true;
                if (IsBetween(p1, p2, q2))                                                              //Is q2 between p1 and p2?
                    return true;
                if (IsBetween(q1, q2, p1))                                                              //Is p11 between q1 and q2?
                    return true;
                return false;
            }
        }

        //Derived from https://stackoverflow.com/a/328122
        private static Boolean IsBetween(MFG.Vector3 a, MFG.Vector3 b, MFG.Vector3 c)
        {
            float crossProduct = (c.y - a.y) * (b.x - a.x) - (c.x - a.x) * (b.y - a.y);
            if (Math.Abs(crossProduct) > 0.000001F)
                return false;
            float dotProduct = (c.x - a.x) * (b.x - a.x) + (c.y - a.y) * (b.y - a.y);
            if (dotProduct < 0)
                return false;
            float squaredLengthBA = (b.x - a.x) * (b.x - a.x) + (b.y - a.y) * (b.y - a.y);
            if (dotProduct > squaredLengthBA)
                return false;
            return true;
        }


        internal static void CollisionProcess()
        {
            foreach (Collision coll in Collisions)
            {
                SceneObject a = coll.a;
                SceneObject b = coll.b;

                string container;
                if (b == null)
                    container = a.GetType().Name + ",NULL";
                else
                    container = a.GetType().Name + "," + b.GetType().Name;

                switch (container)
                    {
                        case "Bullet,NULL":
                            {
                            a.Destroy();
                            }
                        break;
                        case "Tank,Tank":
                            {
                                Bounce(a, b);
                                (a as Tank).Damage();
                                (b as Tank).Damage();
                            }
                            break;
                        case "Bullet,Bullet":
                            {
                                a.Destroy();
                                b.Destroy();
                            }
                            break;
                        case "Turret,Tank":
                            {
                                Bounce(a.Parent, b);
                                (a.Parent as Tank).Damage();
                            }
                            break;
                        case "Tank,Turret":
                            {
                                Bounce(a, b.Parent);
                                (b.Parent as Tank).Damage();
                            }
                            break;
                        case "Turret,Bullet":
                            {
                                (a.Parent as Tank).Damage(10);
                                b.Destroy();
                            }
                            break;
                        case "Bullet,Turret":
                            {
                                a.Destroy();
                                (b.Parent as Tank).Damage(10);
                            }
                            break;
                        case "Tank,Bullet":
                            {
                                (a as Tank).Damage(10);
                                b.Destroy();
                            }
                            break;
                        case "Bullet,Tank":
                            {
                                a.Destroy();
                                (b as Tank).Damage(10);
                            }
                            break;
                        default:
                            Console.WriteLine("Unexpected collision between " + a.GetType().Name + " and " + b.GetType().Name);
                            break;
                    };
            }
            Collisions = null;
        }

        internal static void DrawHealthBars()
        {
            foreach (SceneObject obj in ObjectList)
            {
                if (obj.GetType() != typeof(Tank))
                    continue;

                (obj as Tank).DrawHealthBar();
            }
        }

        internal static void Bounce(SceneObject a, SceneObject b)
        {
            float vel = (a.Velocity.Magnitude() + b.Velocity.Magnitude());
            a.ReboundFrom(b, vel);
            b.ReboundFrom(a, vel);
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

            public Collision(SceneObject a)
            {
                this.a = a;
                this.b = null;
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
