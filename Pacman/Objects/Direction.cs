using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class Direction
    {
        public enum DirectionValue { LEFT, RIGHT, UP, DOWN }

        public static DirectionValue LEFT { get { return DirectionValue.LEFT; } }
        public static DirectionValue RIGHT { get { return DirectionValue.RIGHT; } }
        public static DirectionValue UP { get { return DirectionValue.UP; } }
        public static DirectionValue DOWN { get { return DirectionValue.DOWN; } }

        private DirectionValue _value;

        public DirectionValue Value { get { return _value; } set { _value = value; } }
        public Vector2 Offset { get { return GetOffset(); } }

        public Direction() : this(DirectionValue.LEFT) { }
        public Direction(DirectionValue value)
        {
            _value = value;
        }

        private Vector2 GetOffset()
        {
            if (_value == LEFT) return new Vector2(-1, 0);
            if (_value == RIGHT) return new Vector2(1, 0);
            if (_value == UP) return new Vector2(0, -1);
            if (_value == DOWN) return new Vector2(0, 1);
            return Vector2.Zero;
        }
    }
}
