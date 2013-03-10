using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman
{
    public class Kinematic : IDisposable
    {
        public Kinematic(Vector2 position, Vector2 velocity, float orientation = 0, float rotation = 0)
        {
            Position = position;
            Orienation = orientation;
            Velocity = velocity;
            Rotation = rotation;

            Runtime.GameUpdate += OnGameUpdate;
        }

        public Kinematic(float x = 0, float y = 0, float orientation = 0, float rotation = 0)
            : this(position: new Vector2(x, y), velocity: Vector2.Zero, orientation: orientation, rotation: rotation) { }

        private void OnGameUpdate(GameTime gameTime)
        {
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * time;
            Orienation += Rotation * time;
        }

        public Vector2 Position { get; set; }
        public float Orienation { get; set; }
        public Vector2 Velocity { get; set; }
        public float Rotation { get; set; }

        public float X { get { return Position.X; } set { Position = new Vector2(value, Position.Y); } }
        public float Y { get { return Position.Y; } set { Position = new Vector2(Position.X, value); } }

        public void Dispose()
        {
            Runtime.GameUpdate -= OnGameUpdate;
        }
    }
}
