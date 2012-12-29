using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class Image
    {
        private Texture2D texture;
        private Rectangle sourceRectangle;

        private Vector2 position = Vector2.Zero;
        private Vector2 origin = Vector2.Zero;
        private float orientation = 0.0f;
        private float scale = 1.0f;
        private bool isVisible = true;
        private float alpha = 1.0f;

        public float X { get { return position.X; } set { position.X = value; } }
        public float Y { get { return position.Y; } set { position.Y = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public float Orientation { get { return orientation; } set { orientation = value; } }
        public float Scale { get { return scale; } set { scale = value; } }
        public bool IsVisible { get { return isVisible; } set { isVisible = value; } }
        public int Width { get { return sourceRectangle.Width; } }
        public int Height { get { return sourceRectangle.Height; } }

        public Image(Texture2D texture, Rectangle sourceRectangle)
        {
            this.texture = texture;
            this.sourceRectangle = sourceRectangle;
            setOriginToCenter();
        }

        public Image(Texture2D texture, List<Rectangle> sourceRectangles, int frame) : this(texture, sourceRectangles[frame]) { }
        public Image(Texture2D texture) : this (texture, new Rectangle(0, 0, texture.Width, texture.Height)) { }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            var offsetPosition = Camera.Position + position;
            if (isVisible)
                spriteBatch.Draw(texture, offsetPosition, sourceRectangle, Color.White, orientation, origin, scale, SpriteEffects.None, 0);
        }

        protected void UpdateSourceRectangle(Rectangle newSourceRectangle)
        {
            this.sourceRectangle = newSourceRectangle;
            setOriginToCenter();
        }

        private void setOriginToCenter()
        {
            origin.X = Width / 2;
            origin.Y = Height / 2;
        }
    }
}
