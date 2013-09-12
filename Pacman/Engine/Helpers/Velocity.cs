using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pacman.Engine.Display;

namespace Pacman.Engine.Helpers
{
    class Velocity : GameObject
    {
        Vector2 _velocity;

        public Vector2 Value { get { return _velocity; } set { _velocity = value; } }
        public Position Position { get; set; }
        public Speed Speed { get; set; }

        public Velocity() 
        {
            Position = new Position();
            Speed = new Speed();
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                base.Update(gameTime);
                if (_velocity.Length() > Speed.Value)
                {
                    _velocity.Normalize();
                    _velocity *= Speed.Value;
                }
                Position.Value += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
