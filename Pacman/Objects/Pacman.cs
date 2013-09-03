using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine;
using Microsoft.Xna.Framework;
using Pacman.Engine.Helpers;
using Pacman.Engine.Display;

namespace Pacman.Objects
{
    class Pacman : GroupObject, ISteer
    {
        public Pacman() { }

        public AnimatedSpriteObject AnimatedSprite { get; set; }
        public Speed Speed { get; set; }
        public Velocity Velocity { get; set; }
        public Rotation Rotation { get; set; }
        public Steering Steering { get; set; }
        public TilePosition TilePosition { get; set; }
        public SnapToTarget SnapToTarget { get; set; }
        public Wrap Wrap { get; set; }
        public Direction DesiredDirection { get; set; }
        public Direction PreviousDirection { get; set; }
        public PlayerMovement PlayerMovement { get; set; }
        public Target Target { get; set; }
        public PelletEater PelletEater { get; set; }
        public Position StartPosition { get; set; }
    }
}
