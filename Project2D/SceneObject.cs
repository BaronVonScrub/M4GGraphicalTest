using MathClasses;
using Raylib;
using System;
using System.Collections.Generic;
using static GraphicalTest.GlobalVariables;
using static Raylib.Raylib;
using MFG = MathClasses;

namespace GraphicalTest
{
    class SceneObject
    {
        private bool dirty = false;
        private SceneObject parent = Scene;
        private List<SceneObject> children = new List<SceneObject>();
        protected float scale = 1;
        private float rotation = 0;
        private float rotationShift = 0;

        protected float friction = 0.9F;

        protected MFG.Vector3 position = new MFG.Vector3(0, 0, 1);
        protected MFG.Vector3 velocity = new MFG.Vector3(0, 0, 0);
        protected MFG.Vector3 offset = new MFG.Vector3(0,0,0);
        protected SpriteSet sprites;
        protected Texture2D image;
        internal MFG.Vector3 acceleration = new MFG.Vector3(0, 0, 0);
        protected Matrix3 baseTransform = new Matrix3();
        protected Matrix3 localTransform = new Matrix3();
        protected float MinSpeed = 0.01F;
        internal float maxBoxDimension = float.MaxValue;

        public SceneObject()
        {
            this.parent = null;
        }
        public SceneObject(MFG.Vector3 position, MFG.Vector3 velocity, float rotation, SpriteSet sprites, SceneObject parent)
        {
            this.Position = position;
            this.Velocity = velocity;
            this.Rotation = rotation;
            this.localTransform = TransformMatrix;
            this.sprites = sprites;
            this.parent = parent;
            parent.children.Add(this);
        }

        protected Matrix3 TransformMatrix
        {
            get
            {
                return
                    new Matrix3(1, 0, 0, 0, 1, 0, Position.x, Position.y, 1) *
                    GlobalVariables.RotationMatrix2D(Rotation) *
                    new Matrix3(scale, 0, 0, 0, scale, 0, 0, 0, 1);
            }
        }

        public float RotationShift { set => rotationShift = value; }

        internal void PhysicsRecursive()

        {
            Velocity += acceleration * DeltaTime;
            acceleration = new MFG.Vector3(0, 0, 0);

            Position += Velocity * DeltaTime;
            Velocity *= (float)Math.Pow(1 - friction, DeltaTime);

            Rotation += rotationShift * DeltaTime;

            rotationShift = 0;                                      //Reset it to zero

            foreach (SceneObject child in children)
                child.PhysicsRecursive();
        }

        internal void CollectRecursive(List<SceneObject> received)
        {
            received.Add(this);
            foreach (SceneObject child in children)
                child.CollectRecursive(received);
        }

        public MFG.Vector3 Velocity
        {
            get => velocity;

            set                                                             //Cap speed
            {
                velocity = value;
                if (velocity.Magnitude() < MinSpeed)
                    velocity = new MFG.Vector3(0, 0, 0);
            }
        }

        public MFG.Vector3 Position { get => position;
            set
            {
                if (value == position)
                    return;
                MakeDirty(0);
                position = value;
            }
        }

        public float Rotation {
            get => rotation;
            set
            {
                if (value == rotation)
                    return;
                rotation = (value + 2 * (float)Math.PI) % (2 * (float)Math.PI);
                MakeDirty(0);
            }
        }

        public float GlobalRotation { get => (float)Math.Atan2(GlobalTransform.m2, GlobalTransform.m1); }
        public Matrix3 GlobalTransform { get; set; } = new MFG.Matrix3();

        public void GlobalTransformsRecursive()
        {
 
            if (dirty == false)
                return;
            dirty = false;

            if (parent != null)
            GlobalTransform = parent.GlobalTransform * localTransform;

            foreach (SceneObject child in children)
                child.GlobalTransformsRecursive();
        }

        public void MakeDirty(int level)
        {
            if (dirty == true)
                return;

            //Console.WriteLine("Dirty "+level.ToString());
            dirty = true;

            if (parent != null)
                parent.MakeDirty(level-1);

            foreach (SceneObject child in children)
                child.MakeDirty(level+1);
        }

        internal void LocalTransformsRecursive()
        {
            if (dirty == false)
                return;

            localTransform = baseTransform * TransformMatrix;

            foreach (SceneObject child in children)
                child.LocalTransformsRecursive();
        }

        internal void DrawRecursive()
        {
            DrawTextureEx(image,
                new Vector2(GlobalTransform.m7, GlobalTransform.m8) + ConvertV3ToV2(GlobalVariables.RotationMatrix2D(GlobalRotation)*offset),
                GlobalRotation * (float)(180.0f / Math.PI),
                scale, Color.WHITE);

            foreach (SceneObject child in children)
                child.DrawRecursive();
        }

        internal void DrawDebugRecursive()
        {

            DrawLine((int)GlobalTransform.m7, (int)GlobalTransform.m8, (int)(GlobalTransform.m7 + DistDirToXY(100, GlobalRotation).x), (int)(GlobalTransform.m8 + DistDirToXY(100, GlobalRotation).y), Color.RED);                 // Debug line

            foreach (SceneObject child in children)
                child.DrawDebugRecursive();
        }

        public virtual void PersonalRecursive()
        {
            foreach (SceneObject child in children)
                child.PersonalRecursive();
        }
    }
}
