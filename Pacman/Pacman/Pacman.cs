using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using DisplayEngine;

namespace PacmanGame
{
    class Pacman : SpriteObject, IStatic, IGameObject
    {
        private KinematicSeek kinematicSeek;

        public Vector2 PreviousDirection { get; set; }
        public Vector2 DesiredDirection { get; set; }
        public Vector2 Direction { get; set; }
        public IStatic Target { get { return kinematicSeek.target; } set { kinematicSeek.target = value; } }
        public bool IsSteering { get; set; }

        public Pacman() : base(display.RetrieveTexture("pacman"), display.RetrieveSourceRectangles("pacman"))
        {
            SetFrame(36);
            origin.X = Width / 2;
            origin.Y = Height / 2;
            kinematicSeek = new KinematicSeek() { character = this, target = this, maxSpeed = 250.0f };
            IsSteering = true;
        }

        public override void Update(GameTime gameTime)
        {
            PreviousDirection = Direction;
            SteerToTarget(gameTime);

            base.Update(gameTime);
        }

        private void SteerToTarget(GameTime gameTime)
        {
            if (IsSteering && this.Position != Target.Position)
            {
                var steering = kinematicSeek.GetSteering();
                var time = (float)gameTime.ElapsedGameTime.TotalSeconds;

                var deltaPosition = steering.linear * time;
                var distanceToTarget = (Target.Position - this.position).LengthSquared();
                var distanceBySpeed = deltaPosition.LengthSquared();

                if (distanceToTarget < distanceBySpeed)
                    position = Target.Position;
                else
                    position += deltaPosition;

                Rotation = (float)Math.Atan2((double)-deltaPosition.Y, (double)-deltaPosition.X);
                Rotation += steering.angular * time;
            }
        }
    }
}
