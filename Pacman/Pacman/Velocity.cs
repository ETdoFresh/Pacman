using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class Velocity : IDisposable
    {
        public Vector2 Value { get; set; }
        private Position position;

        public Velocity(Position position, Vector2 value)
        {
            Runtime.GameUpdate += Update;
            Value = value;
            this.position = position;
        }

        public Velocity(Position position) : this(position, Vector2.Zero) { }

        public void Dispose()
        {
            Runtime.GameUpdate -= Update;
        }

        private void Update(GameTime gameTime)
        {
            position.Value += Value * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}