using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTest
{
    class Tank
    {
        float x, y, rot;
        Turret turret;
        List<Bullet> bullets = new List<Bullet>();

        public Tank(float x, float y, float rot, float turretRot)
        {
            this.x = x;
            this.y = y;
            this.rot = rot;
            turret = new Turret(0,0,0);
        }
    }
}
