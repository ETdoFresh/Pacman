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
        public Sprite(String filename, Position position = null, Rotation rotation = null, Scale scale = null, GroupObject parent = null)
            : base(parent, position, rotation, scale) 
        {
            texture = SpriteSheet.GetTexture(filename);
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            origin = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
        }
    }
}
