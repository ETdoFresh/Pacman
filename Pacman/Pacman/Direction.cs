using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    public class Direction
    {
        public DirectionEnum current;

        static public DirectionEnum UP { get { return DirectionEnum.UP; } }
        static public DirectionEnum DOWN { get { return DirectionEnum.DOWN; } }
        static public DirectionEnum LEFT { get { return DirectionEnum.LEFT; } }
        static public DirectionEnum RIGHT { get { return DirectionEnum.RIGHT; } }

        public enum DirectionEnum { UP, DOWN, LEFT, RIGHT }
    }
}
