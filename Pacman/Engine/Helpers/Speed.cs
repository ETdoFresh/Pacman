using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Engine.Helpers
{
    class Speed
    {
        float _speed;

        public float Value { get { return _speed; } set { _speed = value; } }

        public Speed() { }
        public Speed(float value)
        {
            Value = value;
        }
    }
}
