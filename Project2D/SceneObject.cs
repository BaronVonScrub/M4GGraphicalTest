using System;
using System.Collections.Generic;
using MFG = MathClasses;
using System.Text;
using MathClasses;
using static GraphicalTest.GlobalVariables;

namespace GraphicalTest
{
    class SceneObject
    {
        private bool dirty = false;
        private SceneObject parent;
        private List<SceneObject> children = new List<SceneObject>();
        private float scale = 1;
        private float rotation = 0;
        private float rotationShift = 0;

        private Vector3 position = new Vector3(0, 0, 1);
        private Vector3 velocity = new Vector3(0, 0, 0);
        private Vector3 acceleration = new Vector3(0, 0, 0);
        private Matrix3 globalTransform = new Matrix3();
        private Matrix3 baseTransform = new Matrix3();
        private Matrix3 localTransform = new Matrix3();
        private float MaxSpeed = Int32.MaxValue;

        private Matrix3 Scale {
            get
            {
                return new Matrix3(scale,0,0,0,scale,0,0,0,1);
            }
        }

        private Matrix3 Rotate
        {
            get
            {
                return new Matrix3((float)Math.Cos(Rotation), (float)Math.Sin(Rotation),0, (float)-Math.Sin(Rotation), (float)Math.Cos(Rotation),0,0,0,1);
            }
        }
        private Matrix3 Translate
        {
            get
            {
                return new Matrix3(1, 0, 0, 0, 1, 0, position.x, position.y, 1);
            }
        }

        private Matrix3 Transform
        {
            get
            {
                return new Matrix3() * Scale * Rotate * Translate;
            }
        }

        public float RotationShift { set => rotationShift = value; }

        public Vector3 Velocity
        {
            get => velocity;

            set                                                             //Cap speed
            {
                velocity = value;
                if (velocity.Magnitude() > MaxSpeed)
                    velocity = velocity * (MaxSpeed / velocity.Magnitude());
            }
        }

        public Matrix3 GlobalTransform { get; }

        public float Rotation {
            get => rotation;
            set
            {
                rotation = (value + 2 * (float)Math.PI) % (2 * (float)Math.PI);
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
            position += velocity * DeltaTime;
            Rotation += rotationShift * DeltaTime;
            RotationShift = 0;

            if (dirty==true)
                localTransform = baseTransform * Transform;

            foreach (SceneObject child in children)
                child.UpdateLocalTransforms();
        }

        internal void Draw() => throw new NotImplementedException();
    }
}
