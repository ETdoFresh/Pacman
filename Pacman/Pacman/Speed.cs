using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Speed
    {
        private float speed;

        public float Factor { get; set; }
        public float Value { get { return Factor * speed; } set { speed = value; } }

        public Speed(float value, float factor = 1)
        {
            speed = value;
            Factor = factor;
        }
    }
}
