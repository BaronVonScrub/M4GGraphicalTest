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
        private MFG.Matrix3 globalTransform;
        private MFG.Matrix3 localTransform;
        public MFG.Matrix3 GlobalTransform { get => globalTransform; }

        public void UpdateTransforms()
        {
            if (dirty == false)
                return;
            dirty = false;

            globalTransform = parent.GlobalTransform * localTransform;

            foreach (SceneObject child in children)
                child.UpdateTransforms();
        }

        public void MakeDirty()
        {
            dirty = true;

            if (parent != null)
                parent.MakeDirty();
        }

        internal void UpdatePositions() => throw new NotImplementedException();
        internal void Draw() => throw new NotImplementedException();
    }
}
