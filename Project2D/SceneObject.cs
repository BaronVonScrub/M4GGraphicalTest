using Raylib;
using System;
using System.Collections.Generic;
using static GraphicalTest.Global;
using static Raylib.Raylib;
using MFG = MathClassesAidan;

namespace GraphicalTest
{
    class SceneObject
    {
        #region Fields and properties
        //Graphical parent and children
        internal SceneObject Parent { set; get; } = Scene;
        private List<SceneObject> children = new List<SceneObject>();               //The list of graphical children

        //Graphics
        protected float scale = 1;                                                  //The scale it should be drawn at
        private float rotation = 0;                                                 //The rotation it should be drawn at
        protected MFG.Vector3 offset = new MFG.Vector3(0, 0, 0);                    //The visual offset from the position
        protected SpriteSet sprites;                                                //The set of sprites from which the object's sprite is taken
        protected Texture2D image;                                                  //The object's personal sprite
        protected MFG.Matrix3 localTransform = new MFG.Matrix3();                   //The object's graphical transformation relative to its parent
        protected MFG.Matrix3 LocalTransform                                        //Returns an updated version of the localTransform
        {
            get
            {
                return
                    new MFG.Matrix3(1, 0, 0, 0, 1, 0, Position.x, Position.y, 1) *
                    Global.RotationMatrix2D(Rotation) *
                    new MFG.Matrix3(scale, 0, 0, 0, scale, 0, 0, 0, 1);
            }
        }

        //Controlled setters
        public MFG.Vector3 Velocity
        {
            get => velocity;
            set
            {
                velocity = value;
                if (velocity.Magnitude() < MinSpeed)                                //If speed drops below the minimum
                    velocity = new MFG.Vector3(0, 0, 0);                            //Zero it
            }
        }

        public MFG.Vector3 Position
        {
            get => position;
            set
            {
                if (value == position)
                    return;
                MakeDirty();                                                        //If there is a change in position, prep the object for updating
                position = value;
            }
        }

        public float Rotation
        {
            get => rotation;
            set
            {
                if (value == rotation)
                    return;
                rotation = (value + 2 * (float)Math.PI) % (2 * (float)Math.PI);     //Loop rotation to keep its value managable
                MakeDirty();                                                        //If there is a change in rotation, prep the object for updating
            }
        }

        //Update Variables
        private bool dirty = false;                                                 //Should the personal transforms be updated this frame?
        private float rotationShift = 0;                                            //How much should the rotation change right now?
        public float RotationShift { set => rotationShift = value; }                //Public setter for the rotationShift
        protected MFG.Vector3 acceleration = new MFG.Vector3(0, 0, 0);              //How much should the object change velocity right now?

        //Physics update
        protected MFG.Vector3 position = new MFG.Vector3(0, 0, 1);                  //The object's current relative position to its parent
        protected MFG.Vector3 velocity = new MFG.Vector3(0, 0, 0);                  //The object's current relative velocity to its parent
        protected float friction = 0.9F;                                            //The object's tendency to slow to a stop
        protected float MinSpeed = 0.01F;                                           //The minimum speed below which the object will come to a halt
        public MFG.Matrix3 GlobalTransform { get; set; } = new MFG.Matrix3();                                       //Stores the current global transformation matrix for the instance
        public float GlobalRotation { get => (float)Math.Atan2(GlobalTransform.m2, GlobalTransform.m1); }           //Returns the global rotation as defined by GlobalTransform
        public MFG.Vector3 GlobalPosition { get => new MFG.Vector3(GlobalTransform.m7, GlobalTransform.m8, 1); }    //Returns the global position as defined by GlobalTransform

        //Collision detectiom
        private BoundingBox box = new BoundingBox(new MFG.Vector3[0]);              //The boundingbox relative to the object
        internal BoundingBox Box                                                    //The boundingbox, returning global coordinates
        {
            get => new BoundingBox(GlobalTransform * box.vertices);
            set => box = value;
        }
        internal float maxBoxDimension = float.MaxValue;                            //The maximum distance from the boundingbox to the origin (for broad phase collision detection)
        internal List<Type> typeIgnore = new List<Type>();                          //Types the object ignores (For broad phase collision detection)
        internal List<SceneObject> specificIgnore = new List<SceneObject>();        //Specificaly instances the object ignores (for board phase collision detection)

        private Boolean invulnerable = false;                                       //Is the object impervious to the Destroy() method?
        #endregion

        #region Constructors
        //Default constructor; this is only used by the base Scene
        public SceneObject()
        {
            this.Parent = null;
            this.invulnerable = true;
        }

        //More explicit constructor, used by objects that are drawn to the screen and that interact
        public SceneObject(MFG.Vector3 position, MFG.Vector3 velocity, float rotation, SpriteSet sprites, SceneObject parent)
        {
            this.Position = position;
            this.Velocity = velocity;
            this.Rotation = rotation;
            this.localTransform = LocalTransform;
            this.GlobalTransform = parent.GlobalTransform * localTransform;
            this.sprites = sprites;
            this.Parent = parent;

            ObjectList.Add(this);                                                   //Add this to the global list of objects*

            parent.children.Add(this);                                              //Add this to the parent's list of children*
            specificIgnore.Add(parent);                                             //Add the parent to this object's specific list of instances to ignore

            if (parent != Scene)                                                    //If the parent is not the scene itself
                specificIgnore.AddRange(parent.specificIgnore);                     //Ignore everything the parent ignores
            parent.specificIgnore.Add(this);                                        //Add this to the parent's list of instances to ignore*

            //* All of these references must be removed (and any others) in order to destroy the instance
        }
        #endregion

        #region Tail recursive update functions
        //Updates the physics for the current instance, then has all children do the same
        internal void PhysicsRecursive()

        {
            Velocity += acceleration * DeltaTime;                                   //Update velocity
            acceleration = new MFG.Vector3(0, 0, 0);                                //Reset acceleration

            Position += Velocity * DeltaTime;                                       //Update position
            Velocity *= (float)Math.Pow(1 - friction, DeltaTime);                   //Apply friction to velocity

            Rotation += rotationShift * DeltaTime;                                  //Update rotation
            rotationShift = 0;                                                      //Reset rotationshift

            foreach (SceneObject child in children)                                 //Tell children to do the same
                child.PhysicsRecursive();
        }

        //Marks the current object and its lineage for global transform updates.
        //Note that marking the whole lineage ensures it will be reached by the recursive update call on Scene
        public void MakeDirty()
        {
            if (dirty == true)
                return;                                                               //Skip if you're already marked for an update

            dirty = true;                                                             //Mark you for an update

            if (Parent != null)
                Parent.MakeDirty();                                                   //Have your lineage do the same upwards

            foreach (SceneObject child in children)                                   //Have your lineage do the same downwards
                child.MakeDirty();
        }

        //Updates the global transforms for the current instance, then has all children do the same
        public void GlobalTransformsRecursive()
        {

            if (dirty == false)
                return;                                                              //Skip if you're not in need of an update
            dirty = false;

            if (Parent != null)
                GlobalTransform = Parent.GlobalTransform * localTransform;           //Update your global transform according to your parents', and your local

            foreach (SceneObject child in children)                                  //Have your children do the same
                child.GlobalTransformsRecursive();
        }

        //Updates the local transforms, and has its children do the same
        //Given time, I would probably restructure this into a property: If dirty, update and return local transform, otherwise just return transform.
        internal void LocalTransformsRecursive()
        {
            if (dirty == false)
                return;                                                                //Skip if you're not marked for an update

            localTransform = LocalTransform;                                           //Update your local tranform matrix

            foreach (SceneObject child in children)                                    //Have your children do the same
                child.LocalTransformsRecursive();
        }

        //Draw the object, and then its lineage downwards, recursively. This ensures children appear on top of parents.
        internal void DrawRecursive()
        {
            DrawTextureEx(image,
                new Vector2(GlobalTransform.m7, GlobalTransform.m8) + ConvertV3ToV2(Global.RotationMatrix2D(GlobalRotation) * offset * scale),
                GlobalRotation * (float)(180.0f / Math.PI),
                scale, Color.WHITE);                        //Draw the image at the global position modified by the offset

            foreach (SceneObject child in children)
                child.DrawRecursive();
        }

        //This function is unused, but draws lines showing instance bearings, and lines showing their bounding boxes
        internal void DrawDebugRecursive()
        {

            DrawLine((int)GlobalTransform.m7, (int)GlobalTransform.m8, (int)(GlobalTransform.m7 + DistDirToXY(100, GlobalRotation).x), (int)(GlobalTransform.m8 + DistDirToXY(100, GlobalRotation).y), Color.RED);                                                          //Draw a line showing bearings

            for (int i = 0; i < Box.vertices.Length; i++)                          //Draw each component line of the bounding box
            {
                MFG.Vector3 v1 = Box.vertices[i];
                MFG.Vector3 v2 = Box.vertices[(i + 1) % Box.vertices.Length];
                DrawLine((int)v1.x, (int)v1.y, (int)v2.x, (int)v2.y, Color.RED);
            }

            foreach (SceneObject child in children)
                child.DrawDebugRecursive();
        }

        //Recursively processes any unique updates required by subclasses of SceneObject (E.g., tank firing cooldown)
        //Note that this is intended to be overridden, in which you still call this base method
        internal virtual void PersonalRecursive()
        {
            foreach (SceneObject child in children)
                child.PersonalRecursive();
        }
        #endregion

        #region Miscellaneous
        //This function returns the default local boundingbox for the object, based on its image.
        //This is formed from the vertices of each corner of the object's image.
        //Note that non-default, non-rectangular bounding boxes SHOULD work with the collision system, but this has not been tested.
        protected BoundingBox GetDefaultBoundingBox()
        {
            return new BoundingBox(
                new MFG.Vector3[]
                {
                new MFG.Vector3(offset.x, offset.y, 1),
                new MFG.Vector3(image.width + offset.x, offset.y, 1),
                new MFG.Vector3(image.width + offset.x, image.height + offset.y, 1),
                new MFG.Vector3(offset.x, image.height + offset.y, 1)
                }
                );
        }

        //Destroys the object's lineage, including all references, the parent and all children
        internal virtual void Destroy()
        {
            if (invulnerable)                           //Ignore the call if invulnerable
                return;

            Parent.children.Remove(this);               //Remove this from the parent's list of children
            ObjectList.Remove(this);                    //Remove this from the global list of objects
            Parent.specificIgnore.Remove(this);         //Remove this from the parent's list of specific ignores

            if (children.Count != 0)                      //If you have children
                do
                {
                    children[0].Destroy();              //Destroy them each (Note that this must be done before the parent call)
                }
                while (children.Count != 0);            //Until they are gone

            if (Parent != null)                         //If you have a parent
            {
                Parent.Destroy();                       //Destroy that parent
                Parent = null;                          //And remove your reference to it
            }
        }

        //Sets the velocity to amount in direction away from provided object
        internal void ReboundFrom(SceneObject other, float amount)
        {
            Velocity = DistDirToXY(amount, GlobalDirectionTo(other) + (float)Math.PI);
        }

        //Returns the global direction from this object to the provided object
        private float GlobalDirectionTo(SceneObject other)
        {
            return (float)Math.Atan2(other.Position.y - Position.y, other.Position.x - Position.x) - (float)Math.PI / 2;
        }
        #endregion
    }
}