using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Vector3
    {
        public float x, y, z;

        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public Vector3(float xx, float yy, float zz)
        {
            x = xx;
            y = yy;
            z = zz;
        }

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

        public static Vector3 operator *(Matrix3 m, Vector3 v)
        {
            Vector3 newVec = new Vector3
            {
                x = m.m1 * v.x + m.m4 * v.y + m.m7 * v.z,
                y = m.m2 * v.x + m.m5 * v.y + m.m8 * v.z,
                z = m.m3 * v.x + m.m6 * v.y + m.m9 * v.z
            };
            return newVec;
        }

        public float Dot(Vector3 v)
        {
            return x * v.x + y * v.y + z * v.z;
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(x*x+y*y+z*z);
        }

        public void Normalize()
        {
            float mag = Magnitude();
            x /= mag;
            y /= mag;
            z /= mag;
        }

        public Vector3 Cross(Vector3 v)
        {
            Vector3 newVec = new Vector3();

            newVec.x = y * v.z - z * v.y;
            newVec.y = z * v.x - x * v.z;
            newVec.z = x * v.y - y * v.x;

            return newVec;
        }
    }
}
