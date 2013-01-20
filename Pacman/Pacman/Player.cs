using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Pacman
{
    class Player : IGameObject, IStatic
    {
        public static ContentManager Content;

        private Texture2D texture;
        private Rectangle sourceRectangle;
        private Vector2 origin;

        private Vector2 position;
        private Vector2 direction;
        private float orientation;
        private KinematicSeek kinematicSeek;

        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 PreviousDirection { get; set; }
        public Vector2 DesiredDirection { get; set; }
        public Vector2 Direction { get { return direction; } set { direction = value; } }
        public IStatic Target { get { return kinematicSeek.target; } set { kinematicSeek.target = value; } }
        public float Orientation { get { return orientation; } }
        public bool IsSteering { get; set; }

        public Player()
        {
            texture = Content.Load<Texture2D>("pacman");
            var sourceRectangles = TexturePacker.GetTextureRectangles(Content.RootDirectory + "\\pacman.xml");
            sourceRectangle = sourceRectangles[36];
            origin.X = sourceRectangle.Width / 2;
            origin.Y = sourceRectangle.Height / 2;
            kinematicSeek = new KinematicSeek() { character = this, target = this, maxSpeed = 250.0f };
            IsSteering = true;
        }

        public void Update(GameTime gameTime)
        {
            PreviousDirection = Direction;
            SteerToTarget(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, orientation, origin, 1, SpriteEffects.None, 0);
        }

        internal void SetTarget(IStatic target)
        {
            Target = target;
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

                orientation = (float)Math.Atan2((double)-deltaPosition.Y, (double)-deltaPosition.X);
                orientation += steering.angular * time;
            }
        }
    }
}
