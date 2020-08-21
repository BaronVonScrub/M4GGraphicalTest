using Raylib;
using System;
using static GraphicalTest.Global;
using static Raylib.Raylib;
using MFG = MathClassesAidan;

namespace GraphicalTest
{
    class Tank : SceneObject
    {
        #region Fields and properties
        //Construction
        internal Turret turret;                                             //The turret instance for this tank

        //Movement
        static float AccRate = 100F;                                        //The rate at which the tank accelerates
        static float DecRate = 25F;                                         //The rate at which the tank decelerates/goes backwards
        static float TurnSpeed = 1F;                                        //The rate at which the tank turns
        static float TurretSpeed = 1F;                                      //The rate at which the turret turns

        //Combat
        private float cooldown = 3f;                                        //The cooldown on firing a bullet
        private float cooldownCount = 0f;                                   //A counter recording the current cooldown for the bullet
        public float CooldownCount                                          //cooldownCount minned at zero
        {
            get => cooldownCount;
            set => cooldownCount = (float)Math.Max(0, value);
        }
        private int maxHealth = 100;                                        //The maximum health of the tank
        public int Health { get; private set; } = 100;                      //The current health for the tank

        //Extra drawing

        private static Rectangle healthRec = new Rectangle(-30, 50, 60, 10);   //The rectangle storing the dimensions of the healthbar
        //Returns the healthbar rectangle in global coordinates
        public Rectangle HealthRec { get => new Rectangle(healthRec.x + GlobalPosition.x, healthRec.y + GlobalPosition.y, healthRec.width, healthRec.height); }
        //The secondary rectangle (showing current health) in global coordinates
        public Rectangle CurrHealthRec { get => new Rectangle(healthRec.x + GlobalPosition.x, healthRec.y + GlobalPosition.y, healthRec.width / maxHealth * Health, healthRec.height); }
        #endregion

        #region Constructor
        public Tank(MFG.Vector3 position, MFG.Vector3 velocity, float rotation, float turretRot, SpriteSet sprites, SceneObject parent)
            : base(position, velocity, rotation, sprites, parent)
        {
            image = sprites.images[0];                                              //Set the image to the specified index of the spriteset
            offset = new MFG.Vector3(-image.width / 2, -image.height / 2, 0);       //Set the offset as specified
            turret = new Turret(new MFG.Vector3(0, 0, 1), 0, sprites, this);        //Assign it a new turret
            friction = 0.9F;                                                        //Set the friction
            Box = GetDefaultBoundingBox();                                          //Sets the default bounding box
            maxBoxDimension = CalcBoxSize(offset, image);                           //Gets the default bounding box size (modified by offset)
        }
        #endregion

        #region Personal Recursive
        //Overridden personal recursive function, reduces the cooldown of the firing
        internal override void PersonalRecursive()
        {
            CooldownCount -= DeltaTime;
            base.PersonalRecursive();
        }
        #endregion

        #region Tank Commands
        //Implementing commands this way allows them to be easily controlled by either a Player Controller, OR  a hypothetical AI Controller
        internal MFG.Vector3 Forward() => acceleration = DistDirToXY(AccRate, GlobalRotation);
        internal MFG.Vector3 Backward() => acceleration = DistDirToXY(DecRate, GlobalRotation + (float)Math.PI);

        internal float TurnLeft() => RotationShift = -TurnSpeed;
        internal float TurnRight() => RotationShift = TurnSpeed;

        internal float TurretLeft() => turret.RotationShift = -TurretSpeed;
        internal float TurretRight() => turret.RotationShift = TurretSpeed;

        internal void Fire()
        {
            if (CooldownCount != 0)
                return;                                                     //Skip if you can't currently fire

            ReboundFrom(turret.Fire(), 150);                                //Adds some recoil to firing a bullet
            CooldownCount = cooldown;                                       //Sets the cooldown
        }
        #endregion

        #region Health/damage processing

        //Default damage
        internal void Damage()
        {
            Health -= 1;                                        //Reduce health by 1
            if (Health == 0)
                Destroy();                                      //Destroy if you hit 0
        }

        //Damage by a specified amount
        internal void Damage(int amount)
        {
            Health -= amount;                                   //Reduce health by the amount
            if (Health <= 0)
                Destroy();                                      //Destroy if you drop below zero
        }

        //Draw the health bar on the screen
        internal void DrawHealthBar()
        {
            if (Health == maxHealth)                              //Don't draw if you are undamaged
                return;
            DrawRectangleRec(HealthRec, Color.RED);             //Draw a red underlay, the size of the full healthbar
            DrawRectangleRec(CurrHealthRec, Color.GREEN);       //Draw a green overlay, in relation to size of the current health
        }
        #endregion
    }
}
