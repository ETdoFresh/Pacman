using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DisplayLibrary
{
    public class Rotation
    {
        public Rotation(float value = 0)
        {
            Value = value;
        }

        public float Value { get; set; }
    }
}