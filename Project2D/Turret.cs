using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalTest
{
    class Turret
    {
        float relX, relY, rot;
        public Turret(float relX, float relY, float rot)
        {
            this.relX = relX;
            this.relY = relY;
            this.rot = rot;
        }
    }
}
