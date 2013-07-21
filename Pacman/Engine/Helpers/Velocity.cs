using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman.Engine.Helpers
{
    class Velocity : GameObject
    {
        Vector2 _velocity;

        public Vector2 Value { get { return _velocity; } set { _velocity = value; } }
        public Position Position { get; set; }

        public Velocity() : this(Vector2.Zero, new Position()) { }
        public Velocity(Vector2 value) : this(value, new Position()) { }
        public Velocity(Position position) : this(Vector2.Zero, position) { }
        public Velocity(Vector2 value, Position position)
        {
            Value = value;
            Position = position;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Position.Value += Value * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
