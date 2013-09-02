using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;

namespace Pacman.Objects
{
    class LeaveHome : GameObject
    {
        Ghost _ghost;

        public LeaveHome(Ghost ghost)
        {
            _ghost = ghost;
        }
    }
}
