using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.DisplayEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class SteeringObject : SpriteObject
    {
        public Vector2 Velocity { get; set; }
        public DisplayObject Target { get; set; }
        public float Speed { get; set; }
        public bool IsSeeking { get; set; }

        public SteeringObject(Texture2D texture, List<Rectangle> rectangles)
            : base(texture, rectangles)
        {
            Velocity = Vector2.Zero;
            Target = null;
            Speed = 100;
            IsSeeking = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsSeeking)
            {
                SetVelocityTowardTarget();
                UpdatePositionFromVelocity(gameTime);
            }
        }

        private void SetVelocityTowardTarget()
        {
            if (Target != null)
            {
                var newVelocity = Target.Position - Position;
                newVelocity.Normalize();
                Velocity = newVelocity;
            }
        }

        private void UpdatePositionFromVelocity(GameTime gameTime)
        {
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if ((Target.Position - Position).Length() > 5)
                Position += Velocity * time * Speed;
            else
                Velocity = Vector2.Zero;
        }
    }
}
