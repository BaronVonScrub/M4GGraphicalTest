using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClasses
{
    public class Matrix3
    {
        public float[] m = new float[9];

        public float m1 { get => m[0]; set => m[0] = value; }
        public float m2 { get => m[1]; set => m[1] = value; }
        public float m3 { get => m[2]; set => m[2] = value; }
        public float m4 { get => m[3]; set => m[3] = value; }
        public float m5 { get => m[4]; set => m[4] = value; }
        public float m6 { get => m[5]; set => m[5] = value; }
        public float m7 { get => m[6]; set => m[6] = value; }
        public float m8 { get => m[7]; set => m[7] = value; }
        public float m9 { get => m[8]; set => m[8] = value; }

        public Matrix3()
        {
            m1 = 1; m2 = 0; m3 = 0;
            m4 = 0; m5 = 1; m6 = 0;
            m7 = 0; m8 = 0; m9 = 1;
        }

        public Matrix3(float i1, float i2, float i3, float i4, float i5, float i6, float i7, float i8, float i9)
        {
            m1 = i1; m2 = i2; m3 = i3;
            m4 = i4; m5 = i5; m6 = i6;
            m7 = i7; m8 = i8; m9 = i9;
        }

        public Matrix3(float[] i){ m = i; }

        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
        {
            Matrix3 newMat = new Matrix3(new float[9]);
            int dim = 3;

            for (int col = 0; col < dim; col++)
                for (int row = 0; row < dim; row++)
                    for (int term = 0; term < dim; term++)
                        newMat.m[col * dim + row] +=
                            a.m[row+dim*term]*
                            b.m[col*dim+term];

            return newMat;
        }

        public static Boolean operator ==(Matrix3 a, Matrix3 b)
        {
            if (a.m.Length != b.m.Length)
                return false;

            for (int i = 0; i < a.m.Length; i++)
                if (a.m[i] != b.m[i])
                    return false;

            return true;
        }

        public static Boolean operator !=(Matrix3 a, Matrix3 b)
        {
            if (a.m.Length != b.m.Length)
                return true;

            for (int i = 0; i < a.m.Length; i++)
                if (a.m[i] != b.m[i])
                    return true;

            return false;
        }

        public void SetRotateX(float v)
        {
            Matrix3 rot = new Matrix3(1,                 0,                  0,
                                      0,(float)Math.Cos(-v),(float)-Math.Sin(-v),
                                      0,(float)Math.Sin(-v),(float)Math.Cos(-v));

            Matrix3 newMat = this * rot;

            for (int i = 0; i < 9; i++)
                m[i] = newMat.m[i];

        }

        public void SetRotateY(float v)
        {
            Matrix3 rot = new Matrix3((float)Math.Cos(-v), 0, (float)Math.Sin(-v),
                                      0,                  1,                  0,
                                     (float)-Math.Sin(-v), 0, (float)Math.Cos(-v));

            Matrix3 newMat = this * rot;

            for (int i = 0; i < 9; i++)
                m[i] = newMat.m[i];

        }

        public void SetRotateZ(float v)
        {
            Matrix3 rot = new Matrix3((float)Math.Cos(-v), (float)-Math.Sin(-v), 0,
                                      (float)Math.Sin(-v), (float)Math.Cos(-v) , 0,
                                      0,               0,                      1);

            Matrix3 newMat = this * rot;

            for (int i = 0; i < 9; i++)
                m[i] = newMat.m[i];

        }

        public override bool Equals(object obj) => obj is Matrix3 matrix && EqualityComparer<float[]>.Default.Equals(m, matrix.m) && m1 == matrix.m1 && m2 == matrix.m2 && m3 == matrix.m3 && m4 == matrix.m4 && m5 == matrix.m5 && m6 == matrix.m6 && m7 == matrix.m7 && m8 == matrix.m8 && m9 == matrix.m9;

        public override int GetHashCode()
        {
            var hashCode = 2046835463;
            hashCode = hashCode * -1521134295 + EqualityComparer<float[]>.Default.GetHashCode(m);
            hashCode = hashCode * -1521134295 + m1.GetHashCode();
            hashCode = hashCode * -1521134295 + m2.GetHashCode();
            hashCode = hashCode * -1521134295 + m3.GetHashCode();
            hashCode = hashCode * -1521134295 + m4.GetHashCode();
            hashCode = hashCode * -1521134295 + m5.GetHashCode();
            hashCode = hashCode * -1521134295 + m6.GetHashCode();
            hashCode = hashCode * -1521134295 + m7.GetHashCode();
            hashCode = hashCode * -1521134295 + m8.GetHashCode();
            hashCode = hashCode * -1521134295 + m9.GetHashCode();
            return hashCode;
        }
    }
}