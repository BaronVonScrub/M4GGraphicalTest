using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Matrix4
    {
        public float[] m = new float[16];

        public float m1 { get => m[0]; set => m[0] = value; }
        public float m2 { get => m[1]; set => m[1] = value; }
        public float m3 { get => m[2]; set => m[2] = value; }
        public float m4 { get => m[3]; set => m[3] = value; }
        public float m5 { get => m[4]; set => m[4] = value; }
        public float m6 { get => m[5]; set => m[5] = value; }
        public float m7 { get => m[6]; set => m[6] = value; }
        public float m8 { get => m[7]; set => m[7] = value; }
        public float m9 { get => m[8]; set => m[8] = value; }
        public float m10 { get => m[9]; set => m[9] = value; }
        public float m11 { get => m[10]; set => m[10] = value; }
        public float m12 { get => m[11]; set => m[11] = value; }
        public float m13 { get => m[12]; set => m[12] = value; }
        public float m14 { get => m[13]; set => m[13] = value; }
        public float m15 { get => m[14]; set => m[14] = value; }
        public float m16 { get => m[15]; set => m[15] = value; }

        public Matrix4()
        {
            m1 = 1; m2 = 0; m3 = 0; m4 = 0;
            m5 = 0; m6 = 1; m7 = 0; m8 = 0;
            m9 = 0; m10= 0; m11= 1; m12= 0;
            m13= 0; m14= 0; m15= 0; m16= 1;
        }

        public Matrix4(float i1, float i2, float i3, float i4, float i5, float i6, float i7, float i8, float i9)
        {
            m1 = i1; m2 = i2; m3 = i3; m4 = 0;
            m5 = i4; m6 = i5; m7 = i6; m8 = 0;
            m9 = i7; m10 = i8; m11 = i9; m12 = 0;
            m13 = 0; m14 = 0; m15 = 0; m16 = 1;
        }

        public Matrix4(float i1, float i2, float i3, float i4, float i5, float i6, float i7, float i8, float i9, float i10, float i11, float i12, float i13, float i14, float i15, float i16)
        {
            m1 = i1; m2 = i2; m3 = i3; m4 = i4;
            m5 = i5; m6 = i6; m7 = i7; m8 = i8;
            m9 = i9; m10 = i10; m11 = i11; m12 = i12;
            m13 = i13; m14 = i14; m15 = i15; m16 = i16;
        }

        public Matrix4(float[] i){ m = i; }

        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            Matrix4 newMat = new Matrix4(new float[16]);
            int dim = 4;

            for (int col = 0; col < dim; col++)
                for (int row = 0; row < dim; row++)
                    for (int term = 0; term < dim; term++)
                        newMat.m[col * dim + row] +=
                            a.m[row + dim * term] *
                            b.m[col * dim + term];

            return newMat;
        }

        public void SetRotateX(float v)
        {
            Matrix4 rot = new Matrix4(1, 0, 0, 0,
                                      0, (float)Math.Cos(-v), (float)-Math.Sin(-v), 0,
                                      0, (float)Math.Sin(-v), (float)Math.Cos(-v), 0,
                                      0, 0, 0, 1);

            Matrix4 newMat = this * rot;

            for (int i = 0; i < 16; i++)
                m[i] = newMat.m[i];

        }

        public void SetRotateY(float v)
        {
            Matrix4 rot = new Matrix4((float)Math.Cos(-v), 0, (float)Math.Sin(-v), 0,
                                      0, 1, 0, 0,
                                     (float)-Math.Sin(-v), 0, (float)Math.Cos(-v), 0,
                                     0, 0, 0, 1);

            Matrix4 newMat = this * rot;

            for (int i = 0; i < 16; i++)
                m[i] = newMat.m[i];

        }

        public void SetRotateZ(float v)
        {
            Matrix4 rot = new Matrix4((float)Math.Cos(-v), (float)-Math.Sin(-v), 0, 0,
                                      (float)Math.Sin(-v), (float)Math.Cos(-v), 0, 0,
                                      0, 0, 1, 0,
                                      0, 0, 0, 1);

            Matrix4 newMat = this * rot;

            for (int i = 0; i < 16; i++)
                m[i] = newMat.m[i];

        }
    }
}
