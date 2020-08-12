﻿using System;
using System.Collections.Generic;
using static GraphicalTest.Game;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicalTest.GlobalVariables;
using System.IO;
using Raylib;
using static Raylib.Raylib;
using MFG = MathClasses;

namespace GraphicalTest
{
    class Tank : SceneObject
    {
        static float MaxSpeed = 50F;
        static float AccRate = 30F;
        static float DecRate = 15F;
        static float TurnSpeed = 1F;
        static float TurretSpeed = 1F;

        MFG.Vector3 position;
        MFG.Vector3 velocity;
        MFG.Vector3 acceleration;

        internal Turret turret;
        TankSpriteSet sprites;
        Image body;
        float pointDirection;


        public MFG.Vector3 Velocity
        {
            get => velocity;

            set                                                             //Cap speed
            {
                velocity = value;
                if (velocity.Magnitude() > MaxSpeed)
                    velocity = velocity * (MaxSpeed / velocity.Magnitude());
            }
        }

        public float Rotation                                         //Rotation modulo
        {
            get => pointDirection;
            set
            {
                pointDirection = (value + 2 * (float)Math.PI) % (2 * (float)Math.PI);
            }
        }

        public Tank(MFG.Vector3 position, MFG.Vector3 velocity, float rotation, float turretRot, TankSpriteSet sprites)
        {
            this.position = position;
            this.Velocity = velocity;
            this.body = sprites.body;
            turret = new Turret(new MFG.Vector3(0,0,0),0,sprites.turretSet);

            allTanks.Add(this);

            this.Rotation = rotation;
        }

        internal MFG.Vector3 Forward() => acceleration = DistDirToXY(AccRate, Rotation);
        internal MFG.Vector3 Backward() => acceleration = DistDirToXY(DecRate, Rotation+(float)Math.PI);

        internal float TurnLeft() => Rotation += DeltaTime * TurnSpeed;
        internal float TurnRight() => Rotation -= DeltaTime * TurnSpeed;

        internal float TurretLeft() => turret.AimDirection += DeltaTime * TurretSpeed;
        internal float TurretRight() => turret.AimDirection -= DeltaTime * TurretSpeed;

        internal Bullet Fire() => turret.Fire();
    }
}
