using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DisplayLibrary
{
    public class Scale
    {
        public Scale(Vector2 value)
        {
            Value = value;
        }

        public Scale(float x = 1, float y = 1) : this(value: new Vector2(x, y)) { }

        public Vector2 Value { get; set; }
        public float X { get { return Value.X; } set { Value = new Vector2(value, Value.Y); } }
        public float Y { get { return Value.Y; } set { Value = new Vector2(Value.X, value); } }
    }
}
