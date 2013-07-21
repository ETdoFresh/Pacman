using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pacman.Engine.Helpers;

namespace Pacman.Engine
{
    class DisplayObject : GameObject
    {
        public Position Position { get; set; }
        public Rotation Rotation { get; set; }
        public Scale Scale { get; set; }
        public Vector2 Origin { get; protected set; }

        public float Alpha { get; set; }
        public Color Tint { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Vector2 ContentPosition { get { return Parent != null ? Parent.ContentPosition + Position.Value : Position.Value; } }
        public float ContentRotation { get { return Parent != null ? Parent.ContentRotation + Rotation.Value : Rotation.Value; } }
        public float ContentScale { get { return Parent != null ? Parent.ContentScale * Scale.Value : Scale.Value; } }
        public float ContentWidth { get { return Width * ContentScale; } }
        public float ContentHeight { get { return Height * ContentScale; } }

        public DisplayObject() : base()
        {
            Position = new Position();
            Rotation = new Rotation();
            Scale = new Scale(1);
            Alpha = 1;
            Tint = Color.White;
        }

        public void Translate(float x, float y)
        {
            Position.X = x;
            Position.Y = y;
        }

        public void Rotate(float degrees)
        {
            Rotation.Value = MathHelper.ToRadians(degrees);
        }

        public void Resize(float value)
        {
            Scale.Value = value;
        }

        public void SetOrigin(float x, float y)
        {
            Origin = new Vector2(x, y);
        }
    }
}
