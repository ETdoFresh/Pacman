using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman.Engine.Helpers
{
    class Position
    {
        Vector2 _position;

        public Vector2 Value { get { return _position; } set { _position = value; } }
        public float X { get { return _position.X; } set { _position.X = value; } }
        public float Y { get { return _position.Y; } set { _position.Y = value; } }

        public Position() { }

        public Position(Vector2 value)
        {
            Value = value;
        }

        public Position(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Position Copy()
        {
            return new Position(Value);
        }
    }
}
