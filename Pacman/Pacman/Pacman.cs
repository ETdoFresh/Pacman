using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DisplayLibrary;

namespace Pacman
{
    class Pacman : GameObject
    {
        public AnimatedSprite AnimatedSprite { get; set; }
        public Direction Direction { get; set; }
        public PacmanTarget Target { get; set; }
        public Steering Steering { get; set; }
        public SnapToTarget SnapToTarget { get; set; }
        public WrapAroundScreen WrapAroundScreen { get; set; }
        public StartStopAnimation StartStopAnimation { get; set; }

        public Pacman()
        {
        }

    }
}
