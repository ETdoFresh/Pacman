using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Engine.Helpers
{
    class Rotation
    {
        float _rotation;

        public float Value { get { return _rotation; } set { _rotation = value; } }

        public Rotation() { }
        public Rotation(float value)
        {
            Value = value;
        }
    }
}
