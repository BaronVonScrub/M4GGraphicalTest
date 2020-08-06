using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicalTest.Global;
using MFG = MathClasses;


namespace GraphicalTest
{
    class Turret
    {

        private static float length=10;

        MFG.Vector3 relativePosition;
        private MFG.Vector3 aimPosition;
        float aimDirection;

        public float AimDirection { get => aimDirection;
            set
            {
                aimDirection = (value + 360) % 360;
                aimPosition = DistDirToXY(length,aimDirection);
            }
        }

        public Turret(MFG.Vector3 relativePosition, float aimDirection)
        {
            this.relativePosition = relativePosition;
            this.aimDirection = aimDirection;
            allTurrets.Add(this);
        }


        public TurretState Update()
        {
            return new TurretState(relativePosition,aimDirection);
        }

        internal Bullet Fire() => new Bullet(aimPosition,aimDirection);
        internal void Draw()
        {
            
        }
    }
}
