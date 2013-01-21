using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PacmanGame
{
    class Ghost: IGameObject, IStatic
    {
        public static ContentManager Content;

        private Texture2D texture;
        private Rectangle sourceRectangle;
        private Vector2 origin;

        private Vector2 position;
        private KinematicSeek kinematicSeek;

        public Vector2 Position { get { return position; } set { position = value; } }
        public IStatic Target { get { return kinematicSeek.target; } set { kinematicSeek.target = value; } }
        public Tile NextTile { get; set; }
        public Tile NextNextTile { get; set; }
        public float Orientation { get; set; }
        public bool IsSteering { get; set; }

        public Ghost()
        {
            texture = Content.Load<Texture2D>("pacman");
            var sourceRectangles = TexturePacker.GetTextureRectangles(Content.RootDirectory + "\\pacman.xml");
            sourceRectangle = sourceRectangles[0];
            origin.X = sourceRectangle.Width / 2;
            origin.Y = sourceRectangle.Height / 2;
            kinematicSeek = new KinematicSeek() { character = this, target = this, maxSpeed = 150 };
            IsSteering = true;
        }

        public void Update(GameTime gameTime)
        {
            SteerToTarget(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, Orientation, origin, 1, SpriteEffects.None, 0);
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

                Orientation += steering.angular * time;
            }
        }
    }
}
