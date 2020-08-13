﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicalTest.GlobalVariables;
using MFG = MathClasses;
using static Raylib.Raylib;
using Raylib;


namespace GraphicalTest
{
    class Turret : SceneObject
    {
        private static float length=0;
        private float rotation=0;
        private MFG.Vector3 aimPosition;

        public new float Rotation { get => rotation;
            set
            {
                if (value == rotation)
                    return;
                rotation = (value + 2 * (float)Math.PI) % (2 * (float)Math.PI);
                aimPosition = PointOffsetDistDir(length,rotation);
                MakeDirty(0);
            }
        }

        public Turret(MFG.Vector3 position, float rotation, SpriteSet sprites, SceneObject parent)
            : base(position, new MFG.Vector3(0, 0, 0), rotation, sprites, parent)
        {
            MaxSpeed = 0F;
            image = sprites.images[1];
            offset = new MFG.Vector3(-image.width / 2, -image.height / 5, 0);
            length = image.height;
            aimPosition = PointOffsetDistDir(length, rotation);

        }

        internal Bullet Fire() => new Bullet(GlobalTransform*aimPosition,GlobalRotation,sprites);
    }
}
