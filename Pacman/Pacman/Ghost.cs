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
        public Position StartPosition2 { get; set; }
        public AnimatedSprite AnimatedSprite { get; set; }
        public EndTarget EndTarget { get; set; }
        public GeneralTarget Target { get; set; }
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
        public BounceInHome BounceInHome;
        public StopWatch StopWatch { get; set; }
        public DotCounter DotCounter { get; set; }

        public Ghost(GroupObject displayParent = null)
        {
        }

        public override void Dispose()
        {
            ChangeGhostState = null;
            if (AnimatedSprite != null) AnimatedSprite.Dispose();
            if (EndTarget != null) EndTarget.Dispose();
            if (Target != null) Target.Dispose();
            if (Steering != null) Steering.Dispose();
            if (SnapToTarget != null) SnapToTarget.Dispose();
            if (WrapAroundScreen != null) WrapAroundScreen.Dispose();
            if (GetToEndTarget != null) GetToEndTarget.Dispose();
            if (AnimatedTowardDirection != null) AnimatedTowardDirection.Dispose();
            if (BounceInHome != null) BounceInHome.Dispose();
            if (StopWatch != null) StopWatch.Dispose();
            if (DotCounter != null) DotCounter.Dispose();
            base.Dispose();
        }

        public LeaveHome LeaveHome { get; set; }
    }
}
