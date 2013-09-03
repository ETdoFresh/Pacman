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

        public const DirectionValue LEFT = DirectionValue.LEFT;
        public const DirectionValue RIGHT = DirectionValue.RIGHT;
        public const DirectionValue UP = DirectionValue.UP;
        public const DirectionValue DOWN = DirectionValue.DOWN;

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
