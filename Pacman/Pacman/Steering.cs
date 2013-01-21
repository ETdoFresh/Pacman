using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PacmanGame
{
    interface IStatic
    {
        Vector2 Position { get; set; }
        float Orientation { get; set; }
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
