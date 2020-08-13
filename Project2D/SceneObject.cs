﻿using MathClasses;
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
        private float globalRotation = 0;

        protected MFG.Vector3 position = new MFG.Vector3(0, 0, 1);
        protected MFG.Vector3 velocity = new MFG.Vector3(0, 0, 0);
        protected MFG.Vector3 offset = new MFG.Vector3(0,0,0);
        protected SpriteSet sprites;
        protected Texture2D image;
        internal MFG.Vector3 acceleration = new MFG.Vector3(0, 0, 0);
        private Matrix3 globalTransform = new Matrix3();
        protected Matrix3 baseTransform = new Matrix3();
        protected Matrix3 localTransform = new Matrix3();
        protected float MaxSpeed = Int32.MaxValue;
        protected float MinSpeed = 0.01F;

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

        internal void Update_PhysicsRecursive() => throw new NotImplementedException();

        public MFG.Vector3 Velocity
        {
            get => velocity;

            set                                                             //Cap speed
            {
                velocity = value;
                if (velocity.Magnitude() > MaxSpeed)
                    velocity = velocity * (MaxSpeed / velocity.Magnitude());
                else if
                (velocity.Magnitude() < MinSpeed)
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

        public Matrix3 GlobalTransform { get => globalTransform; }

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

        protected float GlobalRotation { get => (float)Math.Atan2(globalTransform.m2, globalTransform.m1); set => globalRotation = value; }

        public void Update_GlobalTransformsRecursive()
        {
            if (dirty == false)
                return;
            dirty = false;

            if (parent != null)
            globalTransform = parent.GlobalTransform * localTransform;

            foreach (SceneObject child in children)
                child.Update_GlobalTransformsRecursive();
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

        internal void Update_LocalTransformsRecursive()
        {
            GlobalRotation = GlobalRotation;

            Velocity += acceleration * DeltaTime;
            acceleration = new MFG.Vector3(0, 0, 0);

            Position += Velocity * DeltaTime;
            Velocity *= (float)Math.Pow(1-friction, DeltaTime);

            Rotation += rotationShift * DeltaTime;

            rotationShift = 0;                                      //Reset it to zero
           

            if (dirty==true)
                localTransform = baseTransform * TransformMatrix;

            GlobalRotation = GlobalRotation;

            foreach (SceneObject child in children)
                child.Update_LocalTransformsRecursive();
        }

        internal void Update_DrawRecursive()
        {
            DrawTextureEx(image,
                new Vector2(globalTransform.m7, globalTransform.m8) + ConvertV3ToV2(GlobalVariables.RotationMatrix2D(GlobalRotation)*offset),
                GlobalRotation * (float)(180.0f / Math.PI),
                scale, Color.WHITE);

            foreach (SceneObject child in children)
                child.Update_DrawRecursive();
        }

        internal void Update_DrawDebugRecursive()
        {

            DrawLine((int)globalTransform.m7, (int)globalTransform.m8, (int)(globalTransform.m7 + DistDirToXY(100, GlobalRotation).x), (int)(globalTransform.m8 + DistDirToXY(100, GlobalRotation).y), Color.RED);                 // Debug line

            foreach (SceneObject child in children)
                child.Update_DrawDebugRecursive();
        }

        public virtual void Update_PersonalRecursive()
        {
            foreach (SceneObject child in children)
                child.Update_PersonalRecursive();
        }
    }
}
