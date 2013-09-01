using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Engine.Helpers
{
    class Orientation
    {
        float _orientation;

        public float Value { get { return _orientation; } set { _orientation = value; } }

        public Orientation() { }
        public Orientation(float value)
        {
            Value = value;
        }
    }
}
