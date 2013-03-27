using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace DisplayLibrary
{
    public class Sprite : DisplayObject
    {
        protected List<Rectangle> sourceRectangles;

        public Sprite(String filename, int index = 0, Position position = null, Rotation rotation = null, Scale scale = null, GroupObject parent = null)
            : base(parent, position, rotation, scale) 
        {
            texture = ContentLoader.GetTexture(filename);
            sourceRectangles = ContentLoader.GetRectangles(filename);

            sourceRectangle = sourceRectangles[index];
            origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
            Width = sourceRectangle.Width;
            Height = sourceRectangle.Height;
        }
    }
}
