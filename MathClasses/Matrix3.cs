using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClassesAidan
{
    public class Matrix3
    {
        //Assigning a matrix with property accessors allows easy manipulation of the data within the unit test requirements
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
        //

        //Default constructor creates an identity matrix
        public Matrix3()
        {
            m1 = 1; m2 = 0; m3 = 0;
            m4 = 0; m5 = 1; m6 = 0;
            m7 = 0; m8 = 0; m9 = 1;
        }

        //Allows initalization of a matrix via float constructor
        public Matrix3(float i1, float i2, float i3, float i4, float i5, float i6, float i7, float i8, float i9)
        {
            m1 = i1; m2 = i2; m3 = i3;
            m4 = i4; m5 = i5; m6 = i6;
            m7 = i7; m8 = i8; m9 = i9;
        }

        //Allows initalization of a matrix via float array constructor
        public Matrix3(float[] i){ m = i; }

        //Allows initalization of a matrix as a copy of another matrix
        public Matrix3(Matrix3 M) { m = M.m; }

        //Allows the mulitplication of two matrices
        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
        {
            Matrix3 newMat = new Matrix3(new float[9]);                 //Creates a zero'ed matrix
            int dim = 3;                                                //The dimension of the matrix is 3 (altering this attribute allows the same code to be used for other sizes)

            for (int col = 0; col < dim; col++)                         //For each column
                for (int row = 0; row < dim; row++)                     //For each row
                    for (int term = 0; term < dim; term++)              //For each term formed by their combining
                        newMat.m[col * dim + row] +=                    //Add the term to the appropriate index in the new matrix
                            a.m[row+dim*term]*
                            b.m[col*dim+term];

            return newMat;                                              //Return the formed matrix
        }

        //Allows premultiplication of a vector by a Matrix
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

        //Returns the transpose of the matrix
        public Matrix3 Transpose()
        {
            Matrix3 trans = new Matrix3();
            for (int pos = 0; pos<9; pos++)
                trans.m[pos]=m[(pos%3)*3+(int)(pos/3)];
            return trans;
        }


        //Sets the rotation of the matrix in the X direction
        public void SetRotateX(float v)
        {
            Matrix3 rot = new Matrix3(1,                 0,                  0,
                                      0,(float)Math.Cos(-v),(float)-Math.Sin(-v),
                                      0,(float)Math.Sin(-v),(float)Math.Cos(-v));

            Matrix3 newMat = this * rot;                                            //Multiply the existing by the rotation matrix

            m = newMat.m;                                                           //Assign the calculated values to the current matrix

        }

        //Rotates Y analagously as above
        public void SetRotateY(float v)
        {
            Matrix3 rot = new Matrix3((float)Math.Cos(-v), 0, (float)Math.Sin(-v),
                                      0,                  1,                  0,
                                     (float)-Math.Sin(-v), 0, (float)Math.Cos(-v));

            Matrix3 newMat = this * rot;

            m = newMat.m;

        }

        //Rotates Z analagously as above
        public void SetRotateZ(float v)
        {
            Matrix3 rot = new Matrix3((float)Math.Cos(-v), (float)-Math.Sin(-v), 0,
                                      (float)Math.Sin(-v), (float)Math.Cos(-v) , 0,
                                      0,               0,                      1);

            Matrix3 newMat = this * rot;

            m = newMat.m;

        }

        //Returns the inversion of the current matrix, or null if it is not invertible
        public Matrix3 GetInverted()
        {
            Matrix3 mat = new Matrix3();

            float det = m1 * m5 * m9 - m1 * m6 * m8 - m2 * m4 * m9 + m2 * m6 * m7 + m3 * m4 * m8 - m3 * m5 * m7;
            if (det == 0)
                return null;    //Not invertible

            float invdet = 1 / det;

            mat.m1 = (m5 * m9 - m8 * m6) * invdet;
            mat.m2 = (m3 * m8 - m2 * m9) * invdet;
            mat.m3 = (m2 * m6 - m3 * m5) * invdet;
            mat.m4 = (m6 * m7 - m4 * m9) * invdet;
            mat.m5 = (m1 * m9 - m3 * m7) * invdet;
            mat.m6 = (m4 * m3 - m1 * m6) * invdet;
            mat.m7 = (m4 * m8 - m7 * m5) * invdet;
            mat.m8 = (m7 * m2 - m1 * m8) * invdet;
            mat.m9 = (m1 * m5 - m4 * m2) * invdet;

            return mat;
        }

        //Returns an array of vectors formed by the multiplication of each vector in the provided array, by the provided matrix
        public static Vector3[] operator *(Matrix3 m, Vector3[] v)
        {
            Vector3[] temp = new Vector3[v.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = m * v[i];
            }
            return temp;
        }
    }
}