using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;

namespace Pacman
{
    class Ghost : GameObject
    {
        public AnimatedSprite AnimatedSprite { get; set; }
        public EndTarget EndTarget { get; set; }
        public NextTile Target { get; set; }
        public Direction Direction { get; set; }
        public Steering Steering { get; set; }
        public GetToEndTarget GetToEndTarget { get; set; }
        public SnapToTarget SnapToTarget { get; set; }
        public WrapAroundScreen WrapAroundScreen { get; set; }
        public AnimatedTowardDirection AnimatedTowardDirection { get; set; }

        public Ghost(GroupObject displayParent = null)
        {
        }
    }
}
