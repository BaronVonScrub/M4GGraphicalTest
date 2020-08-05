using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicalTest.Global;

namespace GraphicalTest
{
    class Bullet
    {
        static float speed = 20;
        float x, y, dir;
        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float Speed { get => speed; set => speed = value; }
        public float Dir { get => dir; set => dir = value; }

        public Bullet(float x, float y, float vel, float dir)
        {
            this.X = x;
            this.Y = y;
            this.Speed = vel;
            this.Dir = dir;
            allBullets.Add(this);
        }


        internal BulletState Update()
        {


            return new BulletState(X, Y, Dir);
        }
    }
}
