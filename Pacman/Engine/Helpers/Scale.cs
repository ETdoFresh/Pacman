using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Engine.Helpers
{
    class Scale
    {
        float _scale;

        public float Value { get { return _scale; } set { _scale = value; } }

        public Scale() { }
        public Scale(float value)
        {
            Value = value;
        }
    }
}
