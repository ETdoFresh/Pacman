using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pacman.Engine.Display;

namespace Pacman.Engine.Helpers
{
    class Rotation : GameObject
    {
        float _orientation;

        public float Value { get { return _orientation; } set { _orientation = value; } }
        public Orientation Orientation { get; set; }

        public Rotation() : this(0, new Orientation()) { }
        public Rotation(float value) : this(value, new Orientation()) { }
        public Rotation(Orientation orientation) : this(0, orientation) { }
        public Rotation(float value, Orientation orientation)
        {
            Value = value;
            Orientation = orientation;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Orientation.Value += Value * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
