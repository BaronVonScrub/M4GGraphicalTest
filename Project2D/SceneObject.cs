using System;
using System.Collections.Generic;
using MFG = MathClasses;
using System.Text;
using MathClasses;
using Raylib;
using static Raylib.Raylib;
using static GraphicalTest.GlobalVariables;

namespace GraphicalTest
{
    class SceneObject
    {
        private bool dirty = false;
        private SceneObject parent = Scene;
        private List<SceneObject> children = new List<SceneObject>();
        private float scale = 1;
        private float rotation = 0;
        private float rotationShift = 0;

        protected MFG.Vector3 position = new MFG.Vector3(0, 0, 1);
        protected MFG.Vector3 velocity = new MFG.Vector3(0, 0, 0);
        protected SpriteSet sprites;
        protected Image image;
        internal MFG.Vector3 acceleration = new MFG.Vector3(0, 0, 0);
        private Matrix3 globalTransform = new Matrix3();
        private Matrix3 baseTransform = new Matrix3();
        private Matrix3 localTransform = new Matrix3();
        protected float MaxSpeed = Int32.MaxValue;

        public SceneObject()
        {
            this.parent = null;
        }
        public SceneObject(MFG.Vector3 position, MFG.Vector3 velocity, float rotation, SpriteSet sprites, SceneObject parent)
        {
            this.Position = position;
            this.Velocity = velocity;
            this.Rotation = rotation;
            this.sprites = sprites;
            this.parent = parent;
        }

        private Matrix3 Transform
        {
            get
            {
                return
                    new Matrix3(scale, 0, 0, 0, scale, 0, 0, 0, 1) *
                    new Matrix3((float)Math.Cos(Rotation), (float)Math.Sin(Rotation), 0, (float)-Math.Sin(Rotation), (float)Math.Cos(Rotation), 0, 0, 0, 1) *
                    new Matrix3(1, 0, 0, 0, 1, 0, Position.x, Position.y, 1);
            }
        }

        public float RotationShift { set => rotationShift = value; }

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

        public MFG.Vector3 Position { get => position;
            set
            {
                if (value == position)
                    return;
                MakeDirty();
                position = value;
            }
        }

        public Matrix3 GlobalTransform { get; }

        public float Rotation {
            get => rotation;
            set
            {
                if (value == rotation)
                    return;
                rotation = (value + 2 * (float)Math.PI) % (2 * (float)Math.PI);
                MakeDirty();
            }
        }

        public void UpdateGlobalTransforms()
        {
            if (dirty == false)
                return;
            dirty = false;

            if (parent != null)
            globalTransform = parent.GlobalTransform * localTransform;

            foreach (SceneObject child in children)
                child.UpdateGlobalTransforms();
        }

        public void MakeDirty()
        {
            dirty = true;

            if (parent != null)
                parent.MakeDirty();

            foreach (SceneObject child in children)
                child.MakeDirty();
        }

        internal void UpdateLocalTransforms()
        {
            Velocity += acceleration * DeltaTime;
            Position += velocity * DeltaTime;
            Rotation += rotationShift * DeltaTime;

            rotationShift = 0;                                      //Reset it to zero

            if (dirty==true)
                localTransform = baseTransform * Transform;

            foreach (SceneObject child in children)
                child.UpdateLocalTransforms();
        }

        internal void Draw()
        {
            //Personal draw code

            foreach (SceneObject child in children)
                child.Draw();
        }
    }
}
