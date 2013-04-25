using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;

namespace Pacman
{
    class Ghost : GameObject
    {
        public Position StartPosition { get; set; }
        public AnimatedSprite AnimatedSprite { get; set; }
        public EndTarget EndTarget { get; set; }
        public NextTile Target { get; set; }
        public Direction Direction { get; set; }
        public Steering Steering { get; set; }
        public GetToEndTarget GetToEndTarget { get; set; }
        public SnapToTarget SnapToTarget { get; set; }
        public WrapAroundScreen WrapAroundScreen { get; set; }
        public AnimatedTowardDirection AnimatedTowardDirection { get; set; }
        private GhostState state;
        public GhostState State { get { return state; } set { state = value; ChangeGhostState(this, state); } }
        public delegate void ChangeGhostStateHandler(Ghost ghost, GhostState state);
        public event ChangeGhostStateHandler ChangeGhostState = delegate { };

        public Ghost(GroupObject displayParent = null)
        {
        }

        public override void Dispose()
        {
            ChangeGhostState = null;
            base.Dispose();
        }
    }
}
