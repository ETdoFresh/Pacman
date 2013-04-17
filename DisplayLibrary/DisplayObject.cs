using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DisplayLibrary
{
    public abstract class DisplayObject : IDisposable
    {
        static public GroupObject Stage = new GroupObject(new Position());
        
        protected Texture2D texture;
        protected Rectangle sourceRectangle;
        protected Vector2 origin;

        public DisplayObject(GroupObject parent, Position position, Dimension dimension, Rotation rotation, Scale scale)
        {
            if (parent == null) parent = Stage;
            if (position == null) position = new Position();
            if (dimension == null) dimension = new Dimension();
            if (rotation == null) rotation = new Rotation();
            if (scale == null) scale = new Scale();

            if (parent != null) parent.Insert(this);

            Parent = parent;
            Position = position;
            Dimension = dimension;
            Rotation = rotation;
            Scale = scale;
            Alpha = 1;
            Color = Color.White;

            IsVisible = true;

            if (this.Parent == null) //this == DisplayObject.Stage
                Runtime.GameDraw += Draw;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, ContentPosition, sourceRectangle, Color * Alpha, ContentRotation, origin, ContentScale, SpriteEffects.None, 0);
        }

        public virtual void Dispose()
        {
            if (this == DisplayObject.Stage)
            {
                Runtime.GameDraw -= Draw;
                Stage = null;
            }

            if (Parent != null)
                Parent.Remove(this, runDipose: false);

            Position = null;
            Rotation = null;
            Scale = null;
        }

        public float Alpha { get; set; }
        public Color Color { get; set; }
        public GroupObject Parent { get; set; }
        public Position Position { get; set; }
        public Rotation Rotation { get; set; }
        public Scale Scale { get; set; }
        public Dimension Dimension { get; set; }
        public Boolean IsVisible { get;set;}
        

        public Vector2 ContentPosition
        {
            get
            {
                return Parent != null ? Position.Value + Parent.ContentPosition : Position.Value;
            }
        }

        public float ContentRotation
        {
            get
            {
                return Parent != null ? Rotation.Value + Parent.ContentRotation : Rotation.Value;
            }
        }

        public Vector2 ContentScale
        {
            get
            {
                return Parent != null ? Scale.Value * Parent.ContentScale : Scale.Value;
            }
        }
    }
}
