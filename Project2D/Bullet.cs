using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicalTest.Global;
using MFG = MathClasses;

namespace GraphicalTest
{
    class Bullet
    {
        private static float speed = 20;
        MFG.Vector3 position, velocity;

        public Bullet(MFG.Vector3 position, float dir)
        {
            this.position = position;
            velocity = new MFG.Vector3(speed, dir);
            allBullets.Add(this);
        }

        internal BulletState Update()
        {
            position += velocity*DeltaTime;
            return new BulletState(position,velocity);
        }
    }
}
