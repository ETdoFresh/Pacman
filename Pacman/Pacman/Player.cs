using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Player : IGameObject, IStatic
    {
        public static ContentManager Content;
        private Texture2D texture;
        private Rectangle sourceRectangle;
        private Vector2 position;
        private Vector2 origin;
        private float orientation;
        private IMovement kinematicSeek;
        private Static target = new Static();

        public Player()
        {
            texture = Content.Load<Texture2D>("pacman");
            var sourceRectangles = TexturePacker.GetTextureRectangles(Content.RootDirectory + "\\pacman.xml");
            sourceRectangle = sourceRectangles[36];
            origin.X = sourceRectangle.Width / 2;
            origin.Y = sourceRectangle.Height / 2;
            kinematicSeek = new KinematicSeek() { character = this, target = target, maxSpeed = 300.0f };
        }

        public Player(Vector2 position) : this() { this.position = position; }

        public void Update(GameTime gameTime)
        {
            if (IsSteering && this.position != target.position)
            {
                var steering = kinematicSeek.GetSteering();
                var time = (float)gameTime.ElapsedGameTime.TotalSeconds;

                var deltaPosition = steering.linear * time;
                var distanceToTarget = (target.position - this.position).LengthSquared();
                var distanceBySpeed = deltaPosition.LengthSquared();

                if (distanceToTarget < distanceBySpeed)
                    position = target.position;
                else
                    position += deltaPosition;

                orientation = (float)Math.Atan2((double)-deltaPosition.Y, (double)-deltaPosition.X);
                orientation += steering.angular * time;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, orientation, origin, 1, SpriteEffects.None, 0);
        }

        public void SetTarget(float x, float y)
        {
            target.position.X = x;
            target.position.Y = y;
        }

        public Vector2 Position { get { return position; } }
        public float Orientation { get { return orientation; } }
        public bool IsSteering { get; set; }
    }
}
