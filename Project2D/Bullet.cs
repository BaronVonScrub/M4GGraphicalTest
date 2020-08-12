using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicalTest.GlobalVariables;
using MFG = MathClasses;
using Raylib;

namespace GraphicalTest
{
    class Bullet : SceneObject
    {
        private static float speed = 20;
        Vector3 position, velocity;
        Image sprite;

        public Bullet(Vector3 position, float dir, Image sprite)
        {
            this.position = position;
            velocity = new Vector3(speed, dir,0);
            this.sprite = sprite;
            allBullets.Add(this);
        }
    }
}
