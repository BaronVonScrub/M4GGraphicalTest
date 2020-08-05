using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Vector4
    {
        public float x, y, z, w;

        public Vector4()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

        public Vector4(float xx, float yy, float zz, float ww)
        {
            x = xx;
            y = yy;
            z = zz;
            w = ww;
        }

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

        public static Vector4 operator *(Matrix4 m, Vector4 v)
        {
            Vector4 newVec = new Vector4
            {
                x = m.m1 * v.x + m.m5 * v.y + m.m9 * v.z + m.m13 * v.w,
                y = m.m2 * v.x + m.m6 * v.y + m.m10 * v.z + m.m14 * v.w,
                z = m.m3 * v.x + m.m7 * v.y + m.m11 * v.z + m.m15 * v.w,
                w = m.m4 * v.x + m.m8 * v.y + m.m12 * v.z + m.m16 * v.w
            };
            return newVec;
        }

        public float Dot(Vector4 v)
        {
            {
                return x * v.x + y * v.y + z * v.z + w * v.w;
            }
        }
        public float Magnitude()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        public void Normalize()
        {
            float mag = Magnitude();
            x /= mag;
            y /= mag;
            z /= mag;
        }

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
