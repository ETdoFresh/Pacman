using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PacmanLibrary.Engine
{
    class DisplayObject
    {
        protected static Stage CurrentStage { get; set; }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }

        public Vector2 ContentPosition 
        { 
            get
            {
                return (Parent != null) ? Position + Parent.ContentPosition : Position;
            }
        }

        public float ContentRotation
        { 
            get
            {
                return (Parent != null) ? Rotation + Parent.ContentRotation : Rotation;
            }
        }

        public Vector2 ContentScale
        {
            get
            {
                return (Parent != null) ? Scale * Parent.ContentScale : Scale;
            }
        }

        public float Width { get; set; }
        public float Height { get; set; }
        public float ContentWidth { get { return Width * ContentScale.X; } }
        public float ContentHeight { get { return Height * ContentScale.Y; } }

        public Vector2 Origin { get; set; }

        public Group Parent { get; set; }

        public string Name { get; set; }

        public float Alpha { get; set; }
        public Color Color { get; set; }
        public bool Visible { get; set; }
        public bool Behind3D { get; set; }

        public DisplayObject(Group parent = null)
        {
            Scale = Vector2.One;
            Alpha = 1.0f;
            Color = Color.White;
            Visible = true;

            if (parent != null)
                Parent = parent;
            else
                Parent = CurrentStage;
        }

        public virtual void Initialize() { }
        public virtual void LoadContent(ContentManager Content) { }
        public virtual void UnloadContent() { }
        public virtual void Update(RenderContext renderContext) { }
        public virtual void Draw(RenderContext renderContext) { }

        // TODO: Implement the following functions
        public Rectangle GetBounds(DisplayObject targetCoordinateSpace) { return Rectangle.Empty; }
        public Rectangle GetRect(DisplayObject targetCoordinateSpace) { return Rectangle.Empty; }
        public bool HitTestObject(DisplayObject obj) { return false; }
        public bool HitTestPoint(float x, float y) { return false; }
    }
}
