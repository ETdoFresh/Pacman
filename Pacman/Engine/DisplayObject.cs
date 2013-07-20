using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman.Engine
{
    class DisplayObject : GameObject
    {
        public Vector2 Position { get; protected set; }
        public float Rotation { get; protected set; }
        public Vector2 Scale { get; protected set; }

        public float Alpha { get; protected set; }
        public Color Tint { get; protected set; }

        public Vector2 ContentPosition { get { return Parent != null ? Parent.Position + Position : Position; } }
        public float ContentRotation { get { return Parent != null ? Parent.Rotation + Rotation : Rotation; } }
        public Vector2 ContentScale { get { return Parent != null ? Parent.Scale + Scale : Scale; } }

        public DisplayObject() : base()
        {
            Scale = Vector2.One;
            Alpha = 1;
            Tint = Color.White;
        }

        public void Translate(float x, float y)
        {
            Position = new Vector2(x, y);
        }

        public void Rotate(float degrees)
        {
            Rotation = degrees;
        }

        public void Resize(float x) { Resize(x, x); }
        public void Resize(float x, float y)
        {
            Scale = new Vector2(x, y);
        }
    }
}
