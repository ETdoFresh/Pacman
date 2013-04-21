using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class Direction
    {
        public enum DirectionEnum { LEFT, RIGHT, UP, DOWN }
        public static DirectionEnum Left = DirectionEnum.LEFT;
        public static DirectionEnum Right = DirectionEnum.RIGHT;
        public static DirectionEnum Up = DirectionEnum.UP;
        public static DirectionEnum Down = DirectionEnum.DOWN;

        private DirectionEnum direction;
        private Keys key;

        public DirectionEnum Value { get { return direction; } set { SetByDirection(value); } }
        public Keys Key { get { return key; } set { SetByKey(value); } }

        public Direction(Keys value)
        {
            SetByKey(value);
        }

        public Direction(DirectionEnum value)
        {
            SetByDirection(value);
        }

        private void SetByKey(Keys key)
        {
            this.key = key;
            if (key == Keys.Up) direction = Up;
            else if (key == Keys.Down) direction = Down;
            else if (key == Keys.Left) direction = Left;
            else if (key == Keys.Right) direction = Right;
        }

        private void SetByDirection(DirectionEnum direction)
        {
            this.direction = direction;
            if (direction == Up) key = Keys.Up;
            else if (direction == Down) key = Keys.Down;
            else if (direction == Left) key = Keys.Left;
            else if (direction == Right) key = Keys.Right;
        }

        public Vector2 GetVectorOffset()
        {
            var offset = Vector2.Zero;

            if (direction == Up) offset.Y--;
            else if (direction == Down) offset.Y++;
            else if (direction == Left) offset.X--;
            else if (direction == Right) offset.X++;
            
            return offset;
        }
    }
}
