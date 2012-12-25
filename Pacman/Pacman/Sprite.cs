using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class Sprite : Animation
    {
        public Vector2 Velocity { get; set; }
        public float Rotation { get; set; }
        public float Speed = 300;
        public Vector2 PreviousPosition;

        public Rectangle BoundingBox { get { return new Rectangle((int)(Position.X - 5), (int)(Position.Y - 5), 10, 10); } }

        public Sprite(Texture2D texture, List<Rectangle> textureRectangles)
            : base(texture, textureRectangles)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            PreviousPosition = Position;
            updateMovement(gameTime);
            WrapAroundScreen();
        }

        private void updateMovement(GameTime gameTime)
        {
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * time * Speed;
            Orientation += Rotation * time * Speed;
        }

        private void WrapAroundScreen()
        {
            var boundsWidth = 800;
            var boundsHeight = 500;

            if (X < 0)
                X = boundsWidth;
            else if (X > boundsWidth)
                X = 0;

            if (Y < 0)
                Y = boundsHeight;
            else if (Y > boundsHeight)
                Y = 0;
        }
    }
}
