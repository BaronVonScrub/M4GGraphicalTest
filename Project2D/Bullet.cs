using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTest
{
    class Bullet
    {
        float x, y, vel, dir;
        public Bullet(float x, float y, float vel, float dir)
        {
            this.x = x;
            this.y = y;
            this.vel = vel;
            this.dir = dir;
        }
    }
}
