using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine;
using Microsoft.Xna.Framework;
using Pacman.Engine.Helpers;

namespace Pacman.Objects
{
    class Steering : GameObject
    {
        public enum Type { Seek }

        ISteer _character;
        ISteer _target;
        KinematicSteeringOutput steeringOutput;

        public Type SteeringType { get; set; }

        public Steering(ISteer character, DisplayObject target) : this(character, new SteerTarget(target)) { }
        public Steering(ISteer character, ISteer target)
        {
            _character = character;
            _target = target;
            SteeringType = Type.Seek;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            steeringOutput = new KinematicSteeringOutput();

            steeringOutput.velocity = _target.Position.Value - _character.Position.Value;

            if (steeringOutput.velocity != Vector2.Zero)
            {
                steeringOutput.velocity.Normalize();
                steeringOutput.velocity *= _character.Speed.Value;
                steeringOutput.orientation = (float)Math.Atan2(-steeringOutput.velocity.Y, -steeringOutput.velocity.X);
            }
            else
            {
                steeringOutput.orientation = _character.Rotation.Value;
            }

            _character.Velocity.Value = steeringOutput.velocity;
            _character.Rotation.Value = steeringOutput.orientation;
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
