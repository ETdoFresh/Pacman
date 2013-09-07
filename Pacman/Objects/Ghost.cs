using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class Ghost : DisplayObject, ISteer
    {
        public enum States { CHASE, SCATTER, FRIGHTENED, HOME, LEAVEHOME }

        public delegate void GhostHandler(Ghost ghost);
        public event GhostHandler StateChange = delegate { };
        private States _state;

        public Ghost() { }

        public override void RemoveSelf()
        {
            if (StateChange != null) StateChange = null;
            if (Body != null) Body.RemoveSelf();
            if (Eyes != null) Eyes.RemoveSelf();
            if (Pupils != null) Pupils.RemoveSelf();
            if (Velocity != null) Velocity.RemoveSelf();
            if (Rotation != null) Rotation.RemoveSelf();
            if (Target != null) Target.RemoveSelf();
            if (Steering != null) Steering.RemoveSelf();
            if (Wrap != null) Wrap.RemoveSelf();
            if (SnapToTarget != null) SnapToTarget.RemoveSelf();
            if (TilePosition != null) TilePosition.RemoveSelf();
            if (ImmediateTarget != null) ImmediateTarget.RemoveSelf();
            if (ShiftEyesToDirection != null) ShiftEyesToDirection.RemoveSelf();
            if (LeaveHome != null) LeaveHome.RemoveSelf();
            base.RemoveSelf();
        }

        public AnimatedSpriteObject Body { get; set; }
        public SpriteObject Eyes { get; set; }
        public SpriteObject Pupils { get; set; }
        public Speed Speed { get; set; }
        public Velocity Velocity { get; set; }
        public Rotation Rotation { get; set; }
        public Direction Direction { get; set; }
        public Target Target { get; set; }
        public Steering Steering { get; set; }
        public Wrap Wrap { get; set; }
        public SnapToTarget SnapToTarget { get; set; }
        public TilePosition TilePosition { get; set; }
        public Target ImmediateTarget { get; set; }
        public ShiftEyesToDirection ShiftEyesToDirection { get; set; }
        public LeaveHome LeaveHome { get; set; }
        
        public States State
        {
            get { return _state; }
            set { _state = value; StateChange(this); }
        }
    }
}
