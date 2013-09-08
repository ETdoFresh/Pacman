using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Helpers;
using Pacman.Engine;

namespace Pacman.Objects
{
    interface ISteer
    {
        Position Position { get; }
        Orientation Orientation { get; }
        Speed Speed { get; }
        Velocity Velocity { get; }
        Rotation Rotation { get; }
    }
}
