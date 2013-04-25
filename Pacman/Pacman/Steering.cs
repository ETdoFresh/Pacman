using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DisplayLibrary;

namespace Pacman
{
    class Steering : IDisposable
    {
        private GameObject character;
        private GameObject target;
        private KinematicSteeringOutput steeringOutput;

        public event EventHandler ArrivedAtTarget;

        public Steering(GameObject character, GameObject target)
        {
            this.character = character;
            this.target = target;

            Runtime.GameUpdate += OnGameUpdate;

            MaxSpeed = 150;
        }

        private void OnGameUpdate(GameTime gameTime)
        {
            steeringOutput = new KinematicSteeringOutput();
            steeringOutput.velocity = target.Position.Value - character.Position.Value;

            if (steeringOutput.velocity != Vector2.Zero)
            {
                steeringOutput.velocity.Normalize();
                steeringOutput.velocity *= MaxSpeed;
                steeringOutput.orientation = (float)Math.Atan2(-steeringOutput.velocity.Y, -steeringOutput.velocity.X);
            }
            else
            {
                if (ArrivedAtTarget != null) ArrivedAtTarget(this, EventArgs.Empty);
                steeringOutput.orientation = character.Rotation.Value;
            }

            character.Velocity.Value = steeringOutput.velocity;
            character.Rotation.Value = steeringOutput.orientation;
        }

        public float MaxSpeed { get; set; }

        public void Dispose()
        {
            ArrivedAtTarget = null;
            Runtime.GameUpdate -= OnGameUpdate;
        }

        public class SteeringOutput
        {
            public Vector2 linear = Vector2.Zero;
            public float angular = 0;
        }

        public class KinematicSteeringOutput
        {
            public Vector2 velocity = Vector2.Zero;
            public float orientation = 0;
        }
    }
}
