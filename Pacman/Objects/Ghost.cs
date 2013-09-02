﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class Ghost : GroupObject, ISteer
    {
        public enum States { CHASE, SCATTER, FRIGHTENED, HOME, LEAVEHOME }

        public delegate void GhostHandler(Ghost ghost);
        public event GhostHandler StateChange = delegate { };
        private States _state;

        public Ghost() { }

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
