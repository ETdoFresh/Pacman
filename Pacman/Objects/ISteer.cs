﻿using System;
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
        Rotation Rotation { get; set; }
        Speed Speed { get; }
        Velocity Velocity { get; set; }
    }
}
