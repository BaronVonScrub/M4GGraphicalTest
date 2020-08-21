using Raylib;
using System;
using System.Collections.Generic;
using static GraphicalTest.Global;
using static Raylib.Raylib;
using MFG = MathClassesAidan;

namespace GraphicalTest
{
    class CollisionManager
    {

        //Stores all detected collisions
        public static List<Collision> Collisions { get; set; } = new List<Collision>();

        //Return all valid collisions between objects in a given list
        internal static void CollisionChecks()
        {
            var collisionList = new List<Collision>();                                                                     //Prep a list

            for (int i = 0; i < ObjectList.Count; i++)                                                                     //For each object
            {
                SceneObject currObj = ObjectList[i];
                {
                    if (currObj.GetType() == typeof(Bullet))                                                              //If it is a bullet
                        if (currObj.GlobalPosition.x < 0 || currObj.GlobalPosition.x > WindowWidth || currObj.GlobalPosition.y < 0 || currObj.GlobalPosition.y > WindowHeight)
                        {                                                                                                 //If it has left the window boundaries
                            collisionList.Add(new Collision(currObj));                                                    //Add a null collision to the list
                            continue;                                                                                     //Continue to next object
                        }

                }
                for (int j = i + 1; j < ObjectList.Count; j++)                                                             //For each object beyond the current
                {
                    SceneObject otherObj = ObjectList[j];

                    if (currObj.typeIgnore.Contains(otherObj.GetType()) || otherObj.typeIgnore.Contains(currObj.GetType())) //If either object ignores the others' type
                        continue;                                                                                           //Skip

                    if (currObj.specificIgnore.Contains(otherObj) || otherObj.specificIgnore.Contains(currObj))             //If either object ignores the other specifically
                        continue;                                                                                           //Skip

                    if (DistanceBetweenObjs(currObj, otherObj) > currObj.maxBoxDimension + otherObj.maxBoxDimension)        //If they are too far apart to actually touch,
                        continue;                                                                                           //Skip

                    if (!Collides(currObj.Box, otherObj.Box))                                                               //If oriented bounding boxes don't collide
                        continue;                                                                                           //Skip

                    collisionList.Add(new Collision(currObj, otherObj));                                                    //Add pair to final list if all checks passed
                }
            }
            Collisions = collisionList;                                                                                     //Update the list of Collisions
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

            for (int i = 0; i < aLen; i++)                                                              //For each vertex comprising the bounding box of the first object
            {
                var lineA = new VecLine(a.vertices[i], a.vertices[(i + 1) % aLen]);                     //Create a line to the next vertex
                for (int j = 0; j < bLen; j++)                                                          //For each vertex comprising the bounding box of the first object
                {
                    var lineB = new VecLine(b.vertices[j], b.vertices[(j + 1) % bLen]);                 //Create a line to the next vertex
                    if (Intersects(lineA, lineB))                                                       //Do the line segments intersect?
                        return true;                                                                    //Yes! So the boxes collide!
                }
            }
            return false;                                                                               //No, none do, so the boxes don't collide.
        }

        //Checks if the provided lines intersect
        //Derived from https://blogs.sas.com/content/iml/2018/07/09/intersection-line-segments.html
        private static bool Intersects(VecLine a, VecLine b)
        {
            //Convert the lines to 4 points
            MFG.Vector3 p1 = a.p;
            MFG.Vector3 p2 = a.p + a.v;
            MFG.Vector3 q1 = b.p;
            MFG.Vector3 q2 = b.p + b.v;

            //Form a matrix from the vector differences between the points
            MFG.Matrix3 A = new MFG.Matrix3((p2 - p1).x, (p2 - p1).y, 0, (q1 - q2).x, (q1 - q2).y, 0, 0, 0, 1);

            //Form a vector from the differences between the start points
            MFG.Vector3 B = q1 - p1;

            //Then
            //A*t = b
            //t = A^-1 * b

            //Get the inverted matrix as required by the previous maths
            MFG.Matrix3 inverted = A.GetInverted();

            if (inverted != null)                                                                       //If it is invertible, they don't share a slope. The lines will intersect.
            {
                MFG.Vector3 solution = inverted * B;                                                    //Solve the equation
                return ((solution.x >= 0 && solution.x <= 1) && (solution.y >= 0 && solution.y <= 1));  //If the solution falls in the unit square, valid collision, else not.
            }
            else                                                                                        //Otherwise, if they share a slope,
            {                                                                                           //Check for overlaps
                if (IsBetween(p1, p2, q1))                                                              //Is q1 between p1 and p2?
                    return true;                                                                        //They collide!
                if (IsBetween(p1, p2, q2))                                                              //Is q2 between p1 and p2?
                    return true;                                                                        //They collide!
                if (IsBetween(q1, q2, p1))                                                              //Is p11 between q1 and q2?
                    return true;                                                                        //They collide!
                return false;                                                                           //If there were no overlaps, they don't collide!
            }
        }

        //Checks if c is collinearly contained within a and b
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

        //Processes the list of collisions
        internal static void CollisionProcess()
        {
            foreach (Collision coll in Collisions)
            {
                SceneObject a = coll.a;
                SceneObject b = coll.b;

                string container;                                                               //Contains info on what types are colliding
                if (b == null)                                                                  //If it is a null collision
                    container = a.GetType().Name + ",NULL";                                     //Note as such
                else                                                                            //Otherwise
                    container = a.GetType().Name + "," + b.GetType().Name;                      //Note both types

                switch (container)                                                              //Look at the info
                {
                    case "Bullet,NULL":                                                     //case "x,y": Read these cases as "I am an x colliding with a y"
                        {
                            a.Destroy();
                        }
                        break;
                    case "Tank,Tank":                                                       //If two tanks collide
                        {
                            Bounce(a, b);                                                   //Bounce the tanks off of each other (Easy to stop repeat collisions)
                            (a as Tank).Damage();                                           //Damage both tanks
                            (b as Tank).Damage();
                        }
                        break;
                    case "Bullet,Bullet":                                                   //If two bullets collide, destroy them both
                        {
                            a.Destroy();
                            b.Destroy();
                        }
                        break;
                    case "Turret,Tank":                                                     //If a turret collides with a tank, bounce the tanks, and damage the turret's tank.
                        {
                            Bounce(a.Parent, b);
                            (a.Parent as Tank).Damage();
                        }
                        break;
                    case "Tank,Turret":                                                     //Inverted case of previous
                        {
                            Bounce(a, b.Parent);
                            (b.Parent as Tank).Damage();
                        }
                        break;
                    case "Turret,Bullet":                                                   //If a turret collides with a bullet, damage the turret's tank, destroy the bullet
                        {
                            (a.Parent as Tank).Damage(10);
                            b.Destroy();
                        }
                        break;
                    case "Bullet,Turret":                                                   //Inverted case of previous
                        {
                            a.Destroy();
                            (b.Parent as Tank).Damage(10);
                        }
                        break;
                    case "Tank,Bullet":                                                     //If a tank collides with a bullet, damage the tank, destroy the bullet
                        {
                            (a as Tank).Damage(10);
                            b.Destroy();
                        }
                        break;
                    case "Bullet,Tank":                                                     //Inverted case of previous
                        {
                            a.Destroy();
                            (b as Tank).Damage(10);
                        }
                        break;
                    default:                                                                //If anything else
                        Console.WriteLine("Unexpected collision between " + a.GetType().Name + " and " + b.GetType().Name); //Record unexepected collision
                        break;
                };
            }
            Collisions = null;                                                                  //Clear the current list of collisions
        }

        //Bounce two objects off of each other; does not conserve momentum, so not physically accurate, but useful!
        internal static void Bounce(SceneObject a, SceneObject b)
        {
            float vel = (a.Velocity.Magnitude() + b.Velocity.Magnitude());
            a.ReboundFrom(b, vel);
            b.ReboundFrom(a, vel);
        }

        //Stores a line as a start point and a vector
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

        //Stores a collision by remembering the two objects involved
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

        //Returns the distance between object a and obejct b
        internal static float DistanceBetweenObjs(SceneObject a, SceneObject b)
        {
            float xx = Math.Abs(a.GlobalPosition.x - b.GlobalPosition.x);
            float yy = Math.Abs(a.GlobalPosition.y - b.GlobalPosition.y);
            return (float)Math.Sqrt(xx * xx + yy * yy);

        }
    }

    //Stores a list of textures as created from an image array provided
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

    //Stores a bounding box as an array of Vector3 vertices
    internal struct BoundingBox
    {
        internal MFG.Vector3[] vertices;
        internal BoundingBox(MFG.Vector3[] vertices) => this.vertices = vertices;
    }
}
