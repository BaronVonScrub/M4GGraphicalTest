using System;
using System.Collections.Generic;
using MFG = MathClasses;
using System.Text;

namespace GraphicalTest
{
    class SceneObject
    {
        private bool dirty = false;
        private SceneObject parent;
        private List<SceneObject> children = new List<SceneObject>();
        private MFG.Vector3 position = new MFG.Vector3(0, 0, 1);
        private MFG.Vector3 velocity = new MFG.Vector3(0, 0, 0);
        private MFG.Vector3 acceleration = new MFG.Vector3(0, 0, 0);
        private MFG.Matrix3 globalTransform = new MFG.Matrix3(0, 0, 0, 0, 0, 0, 0, 0, 0);
        private MFG.Matrix3 localTransform = new MFG.Matrix3(0,0,0,0,0,0,0,0,0);
        public MFG.Matrix3 GlobalTransform { get => globalTransform; }

        public void UpdateTransforms()
        {
            if (dirty == false)
                return;
            dirty = false;

            if (parent != null)
            globalTransform = parent.GlobalTransform * localTransform;

            foreach (SceneObject child in children)
                child.UpdateTransforms();
        }

        public void MakeDirty()
        {
            dirty = true;

            if (parent != null)
                parent.MakeDirty();

            foreach (SceneObject child in children)
                child.MakeDirty();
        }

        internal void UpdatePositions()
        {
            velocity += acceleration;
            position += velocity;
        }

        internal void Draw() => throw new NotImplementedException();
    }
}
