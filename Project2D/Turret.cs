using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicalTest.Global;

namespace GraphicalTest
{
    class Turret
    {
        float relX, relY, dir;

        public float RelX { get => relX; set => relX = value; }
        public float RelY { get => relY; set => relY = value; }
        public float Dir { get => dir; set => dir = value; }

        public Turret(float relX, float relY, float dir)
        {
            this.RelX = relX;
            this.RelY = relY;
            this.Dir = dir;
            allTurrets.Add(this);
        }

        public TurretState Update()
        {
            return new TurretState(RelX, RelY, Dir);
        }


    }
}
