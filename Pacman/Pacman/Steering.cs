using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman
{
    interface IStatic
    {
        Vector2 Position { get; }
        float Orientation { get; }
    }

    class Static : IStatic
    {
        public Vector2 position = Vector2.Zero;
        public float orientation = 0;

        public Vector2 Position { get { return position; } }
        public float Orientation { get { return orientation; } }
    }

    interface IMovement
    {
        SteeringOutput GetSteering();
    }

    class KinematicSeek : IMovement
    {
        public IStatic character;
        public IStatic target;
        public float maxSpeed;

        public SteeringOutput GetSteering()
        {
            var steering = new SteeringOutput();
            steering.linear = target.Position - character.Position;
            if (steering.linear != Vector2.Zero)
            {
                steering.linear.Normalize();
                steering.linear *= maxSpeed;
            }
            steering.angular = 0;
            return steering;
        }
    }

    struct SteeringOutput
    {
        public Vector2 linear;
        public float angular;
    }
}
