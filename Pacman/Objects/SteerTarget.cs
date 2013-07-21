using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Helpers;
using Pacman.Engine;
using Pacman.Engine.Display;

namespace Pacman.Objects
{
    class SteerTarget : ISteer
    {
        public Position Position { get; set; }
        public Rotation Rotation { get; set; }
        public Speed Speed { get; set; }
        public Velocity Velocity { get; set; }

        public SteerTarget(DisplayObject displayObject)
        {
            Position = displayObject.Position;
            Rotation = displayObject.Rotation;
            Speed = new Speed();
        }
    }
}
