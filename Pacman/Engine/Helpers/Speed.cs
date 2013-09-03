using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Engine.Helpers
{
    class Speed
    {
        float _speed;

        public float Factor { get; set; }
        public float Value { get { return Factor * _speed; } set { _speed = value; } }

        public Speed() : this(0, 1) { }
        public Speed(float value) : this(value, 1) { }
        public Speed(float value, float factor)
        {
            Value = value;
            Factor = factor;
        }
    }
}
