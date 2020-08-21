using System;
using System.Collections.Generic;
using static GraphicalTest.Global;
using MFG = MathClassesAidan;
namespace GraphicalTest
{
    class Bullet : SceneObject
    {
        //Sets the speed
        private static float speed = 1000;

        //Constructor calls base constructor, setting velocity to speed in rotation direction, and parented to Scene
        public Bullet(MFG.Vector3 position, float rotation, SpriteSet sprites, List<SceneObject> ignoreList, SceneObject ignoreParent) : base(position, DistDirToXY(speed, rotation), rotation + (float)Math.PI, sprites, Scene)
        {
            image = sprites.images[2];                                          //Sets the image out of the spriteset
            friction = 0;                                                       //No friction
            offset = new MFG.Vector3(-image.width / 2, -image.height / 2, 0);   //Sets the offset for the bullet (Centered)
            scale = 1F;                                                         //Full scale
            maxBoxDimension = CalcBoxSize(offset, image);                       //Returns the maxiumum size of the box dimension (NOTE THAT THIS ONLY WORKS WITH DEFAULT BOXES)
            Box = GetDefaultBoundingBox();                                      //Sets the default rectangular bounding box
            specificIgnore.AddRange(ignoreList.FindAll(x => (x != Scene)));         //Finds all non-Scene sceneObjects in the provided list, and adds them to the ignorelist
            specificIgnore.Add(ignoreParent);                                   //Adds the creator (not parent) to the ignorelist
        }
    }
}
