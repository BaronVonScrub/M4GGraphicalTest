using System;
using static GraphicalTest.Global;
using MFG = MathClassesAidan;

namespace GraphicalTest
{
    class Turret : SceneObject
    {
        private static float length = 0;                  //The length of the barrel, used for calculating where its bullets spawn
        private float rotation = 0;                       //The current rotation of the turret (Different from parent, as it updates AimPosition)
        private MFG.Vector3 aimPosition;                //The relative position a bullet will be fired from

        public new float Rotation
        {
            get => rotation;
            set
            {
                if (value == rotation)
                    return;
                rotation = (value + 2 * (float)Math.PI) % (2 * (float)Math.PI);         //Control the rotation's value
                AimPosition = PointOffsetDistDir(length, rotation);                      //Update the AimPosition
                MakeDirty();                                                            //Queue it for an update
            }
        }

        public MFG.Vector3 AimPosition { get => GlobalTransform * aimPosition; set => aimPosition = value; }  //Getter first processes the local position into a global position

        //Constructor
        public Turret(MFG.Vector3 position, float rotation, SpriteSet sprites, SceneObject parent)
            : base(position, new MFG.Vector3(0, 0, 0), rotation, sprites, parent)
        {
            image = sprites.images[1];                                                  //Set image to the specified index of the spriteset
            offset = new MFG.Vector3(-image.width / 2, -image.height / 5, 0);           //Set the offset as specified
            length = image.height * scale;                                                //Set the length to the appropriate value
            AimPosition = PointOffsetDistDir(length, rotation);                         //Set the initial AimPosition
            Box = GetDefaultBoundingBox();                                              //Set the default bounding box
            maxBoxDimension = CalcBoxSize(offset, image);                               //Get the default bounding box size
        }

        //Fire a bullet
        internal Bullet Fire() => new Bullet(AimPosition, GlobalRotation, sprites, specificIgnore, this);
    }
}
