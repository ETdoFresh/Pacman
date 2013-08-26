using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Helpers;
using Pacman.Engine.Display;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class Target : CircleObject, ISteer
    {
        public Speed Speed { get; set; }
        public Velocity Velocity { get; set; }

        public Target() : base(10) 
        {
            Alpha = 0.5f;
            Tint = Color.Yellow;
            Speed = new Speed(100);
            Velocity = new Velocity(Position);
        }
    }
}
