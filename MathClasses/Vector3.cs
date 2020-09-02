using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClassesAidan
{
    public class Vector3
    {
        //Stored values
        public float x, y, z;

        //Base constructor is a zero vector
        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        //Allows construction via floats
        public Vector3(float xx, float yy, float zz)
        {
            x = xx;
            y = yy;
            z = zz;
        }

        //Allows addition of two vectors
        public static Vector3 operator+ (Vector3 a, Vector3 b)
        {
            Vector3 newVec = new Vector3
            {
                x = a.x + b.x,
                y = a.y + b.y,
                z = a.z + b.z
            };
            return newVec;
        }

        //Allows subtraction of 2 vectors
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            Vector3 newVec = new Vector3
            {
                x = a.x - b.x,
                y = a.y - b.y,
                z = a.z - b.z
            };
            return newVec;
        }

        //Allows multiplication of a vector by a scalar, vector first
        public static Vector3 operator *(Vector3 a, float b)
        {
            Vector3 newVec = new Vector3
            {
                x = a.x * b,
                y = a.y * b,
                z = a.z * b
            };
            return newVec;
        }

        //Allows multiplication of a vector by a scalar, scalar first
        public static Vector3 operator *(float b, Vector3 a)
        {
            Vector3 newVec = new Vector3
            {
                x = a.x * b,
                y = a.y * b,
                z = a.z * b
            };
            return newVec;
        }

        //Checks for equality of a matrix
        public static Boolean operator ==(Vector3 a, Vector3 b)
        {
            if (a.x == b.x && a.y == b.y && a.z == b.z)
                return true;
            return false;
        }

        //Checks for inequality of a matrix
        public static Boolean operator !=(Vector3 a, Vector3 b)
        {
            if (a.x != b.x || a.y != b.y || a.z != b.z)
                return true;
            return false;
        }

        //Returns the dot product of the matrix
        public float Dot(Vector3 v)
        {
            return x * v.x + y * v.y + z * v.z;
        }

        //Returns the magnitude of the matrix
        public float Magnitude()
        {
            return (float)Math.Sqrt(x*x+y*y+z*z);
        }

        //Normalizes the current matrix to magnitude 1
        public void Normalize()
        {
            float mag = Magnitude();
            x /= mag;
            y /= mag;
            z /= mag;
        }

        //Returns the cross product of the current vector
        public Vector3 Cross(Vector3 v)
        {
            Vector3 newVec = new Vector3
            {
                x = y * v.z - z * v.y,
                y = z * v.x - x * v.z,
                z = x * v.y - y * v.x
            };

            return newVec;
        }

        //Checks for equality of the vector with another
        public override bool Equals(object obj) => obj is Vector3 vector && x == vector.x && y == vector.y && z == vector.z;

        //returns the hashcode of the vector
        public override int GetHashCode()
        {
            var hashCode = 373119288;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + z.GetHashCode();
            return hashCode;
        }
    }
}
