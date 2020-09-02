using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClassesAidan
{
    public class Vector4
    {
        //Stored values
        public float x, y, z, w;

        //Base constructor is a zero vector
        public Vector4()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

        //Allows construction via floats
        public Vector4(float xx, float yy, float zz, float ww)
        {
            x = xx;
            y = yy;
            z = zz;
            w = ww;
        }

        //Allows addition of two vectors
        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            Vector4 newVec = new Vector4
            {
                x = a.x + b.x,
                y = a.y + b.y,
                z = a.z + b.z,
                w = a.w + b.w
            };
            return newVec;
        }

        //Allows subtraction of two vectors
        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            Vector4 newVec = new Vector4
            {
                x = a.x - b.x,
                y = a.y - b.y,
                z = a.z - b.z,
                w = a.w - b.w
            };
            return newVec;
        }

        //Allows multiplication of a vector by a scalar, vector first
        public static Vector4 operator *(Vector4 a, float b)
        {
            Vector4 newVec = new Vector4
            {
                x = a.x * b,
                y = a.y * b,
                z = a.z * b,
                w = a.w * b
            };
            return newVec;
        }

        //Allows multiplication of a vector by a scalar, scalar first
        public static Vector4 operator *(float b, Vector4 a)
        {
            Vector4 newVec = new Vector4
            {
                x = a.x * b,
                y = a.y * b,
                z = a.z * b,
                w = a.w * b
            };
            return newVec;
        }

        //Returns the dot product of the matrix
        public float Dot(Vector4 v)
        {
            {
                return x * v.x + y * v.y + z * v.z + w * v.w;
            }
        }

        //Returns the magnitude of the matrix
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z + w * w);
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
        public Vector4 Cross(Vector4 v)
        {
            Vector4 newVec = new Vector4();

            newVec.x = y * v.z - z * v.y;
            newVec.y = z * v.x - x * v.z;
            newVec.z = x * v.y - y * v.x;
            newVec.w = 0;

            return newVec;
        }

    }
}
