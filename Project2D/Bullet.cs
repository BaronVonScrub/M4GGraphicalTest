using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicalTest.Global;
using MFG = MathClasses;
using Raylib;

namespace GraphicalTest
{
    class Bullet
    {
        private static float speed = 20;
        MFG.Vector3 position, velocity;
        Image sprite;

        public Bullet(MFG.Vector3 position, float dir, Image sprite)
        {
            this.position = position;
            velocity = new MFG.Vector3(speed, dir,0);
            this.sprite = sprite;
            allBullets.Add(this);
        }

        internal BulletState Update()
        {
            position += velocity*DeltaTime;
            return new BulletState(position,velocity);
        }

        internal void Draw() => throw new NotImplementedException();
    }
}
