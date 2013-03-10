using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman
{
    public class Steering : IDisposable
    {
        private Kinematic character;
        private Kinematic target;
        private KinematicSteeringOutput steeringOutput;

        public event EventHandler ArrivedAtTarget;

        public Steering(Kinematic character, Kinematic target)
        {
            this.character = character;
            this.target = target;

            Runtime.GameUpdate += OnGameUpdate;

            MaxSpeed = 100;
        }

        private void OnGameUpdate(GameTime gameTime)
        {
            steeringOutput = new KinematicSteeringOutput();
            steeringOutput.velocity = target.Position - character.Position;
            
            if (steeringOutput.velocity != Vector2.Zero)
            {
                steeringOutput.velocity.Normalize();
                steeringOutput.velocity *= MaxSpeed;
                steeringOutput.orientation = (float)Math.Atan2(-steeringOutput.velocity.Y, -steeringOutput.velocity.X);
            }
            else
            {
                if (ArrivedAtTarget != null) ArrivedAtTarget(this, EventArgs.Empty);
                steeringOutput.orientation = character.Orienation;
            }

            character.Velocity = steeringOutput.velocity;
            character.Orienation = steeringOutput.orientation;
        }

        public float MaxSpeed { get; set; }

        public void Dispose()
        {
            Runtime.GameUpdate -= OnGameUpdate;
        }

        public class SteeringOutput
        {
            public Vector2 linear = Vector2.Zero;
            private float angular = 0;
        }

        public class KinematicSteeringOutput
        {
            public Vector2 velocity = Vector2.Zero;
            public float orientation = 0;
        }
    }
}
