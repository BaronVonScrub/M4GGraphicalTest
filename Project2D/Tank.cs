using System;
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
        static float AccRate = 100F;
        static float DecRate = 25F;
        static float TurnSpeed = 1F;
        static float TurretSpeed = 1F;
        private float cooldown = 3f;
        private float cooldownCount = 0f;

        private static Rectangle healthRec = new Rectangle(-30,50,60,10);

        internal Turret turret;
        private int maxHealth = 100;

        public float CooldownCount {
            get => cooldownCount;
            set => cooldownCount = (float)Math.Max(0, value);
        }
        public int Health { get; private set; } = 100;
        public Rectangle HealthRec { get => new Rectangle(healthRec.x+GlobalPosition.x, healthRec.y + GlobalPosition.y,healthRec.width,healthRec.height); }
        public Rectangle CurrHealthRec { get => new Rectangle(healthRec.x + GlobalPosition.x, healthRec.y + GlobalPosition.y, healthRec.width/maxHealth*Health, healthRec.height); }

        public Tank(MFG.Vector3 position, MFG.Vector3 velocity, float rotation, float turretRot, SpriteSet sprites, SceneObject parent)
            : base(position, velocity, rotation, sprites, parent)
        {
            image = sprites.images[0];
            offset = new MFG.Vector3(-image.width / 2, -image.height / 2, 0);
            turret = new Turret(new MFG.Vector3(0, 0, 1), 0, sprites, this);
            friction = 0.9F;
            maxBoxDimension = CalcBoxSize(offset, image);
            Box = GetDefaultBoundingBox();
        }

        internal override void PersonalRecursive()
        {
            CooldownCount -= DeltaTime;
            base.PersonalRecursive();
        }

        internal  MFG.Vector3 Forward() => acceleration = DistDirToXY(AccRate, GlobalRotation);
        internal  MFG.Vector3 Backward() => acceleration = DistDirToXY(DecRate, GlobalRotation+(float)Math.PI);

        internal float TurnLeft() => RotationShift = -TurnSpeed;
        internal float TurnRight() => RotationShift = TurnSpeed;

        internal float TurretLeft() => turret.RotationShift = -TurretSpeed;
        internal float TurretRight() => turret.RotationShift = TurretSpeed;

        internal void Fire()
        {
            if (CooldownCount != 0)
                return;

            ReboundFrom(turret.Fire(), 150);
            CooldownCount = cooldown;
        }

        internal void Damage()
        {
            Health -= 1;
            if (Health == 0)
                Destroy();
        }

        internal void Damage(int amount)
        {
            Health -= amount;
            if (Health <= 0)
                Destroy();
        }

        internal void DrawHealthBar()
        {
            if (Health==maxHealth)
                return;
            DrawRectangleRec(HealthRec, Color.RED);
            DrawRectangleRec(CurrHealthRec, Color.GREEN);
        }
    }
}
