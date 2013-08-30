using System;
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
        public Ghost() { }

        public AnimatedSpriteObject Body { get; set; }
        public AnimatedSpriteObject Eyes { get; set; }
        public AnimatedSpriteObject Pupils { get; set; }
        public Speed Speed { get; set; }
        public Velocity Velocity { get; set; }
        public Direction Direction { get; set; }

        public Target Target { get; set; }
    }
}
