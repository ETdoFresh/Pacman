using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DisplayLibrary
{
    public class Dimension
    {
        public Dimension(Vector2 value)
        {
            Value = value;
        }

        public Dimension(float width = 0, float height = 0) : this(value: new Vector2(width, height)) { }

        public Vector2 Value { get; set; }
        public float Width { get { return Value.X; } set { Value = new Vector2(value, Value.Y); } }
        public float Height { get { return Value.Y; } set { Value = new Vector2(Value.X, value); } }
    }
}
