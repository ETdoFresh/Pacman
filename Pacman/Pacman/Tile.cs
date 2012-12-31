using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class Tile : IGameObject
    {
        private Texture2D texture;
        private List<Rectangle> sourceRectangles;
        private Rectangle destinationRectangle;
        private Rectangle sourceRectangle;
        private float orientation;
        private Vector2 origin;
        private float x;
        private float y;
        private int width;
        private int height;

        public float X { get { return x; } set { x = value; UpdateDestination(); } }
        public float Y { get { return y; } set { y = value; UpdateDestination(); } }
        public int Width { get { return width; } set { width = value; UpdateDestination(); CenterOrigin(); } }
        public int Height { get { return height; } set { height = value; UpdateDestination(); CenterOrigin(); } }
        public float Orientation { get { return orientation; } set { orientation = value; UpdateDestination(); } }
        public bool IsVisible { get; set; }

        public Tile(Texture2D texture, List<Rectangle> sourceRectangles, int textureIndex)
        {
            this.texture = texture;
            this.sourceRectangles = sourceRectangles;
            this.sourceRectangle = sourceRectangles[textureIndex];

            Width = sourceRectangle.Width;
            Height = sourceRectangle.Height;
            IsVisible = true;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                var tint = Color.White;
                var spriteEffect = SpriteEffects.None;
                var layerDepth = 0.0f;
                spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, tint, orientation, origin, spriteEffect, layerDepth);
            }
        }

        public void ChangeTexture(int textureIndex)
        {
            sourceRectangle = sourceRectangles[textureIndex];
        }

        private void UpdateDestination()
        {
            destinationRectangle = new Rectangle((int)X, (int)Y, Width, Height);
        }

        private void CenterOrigin()
        {
            origin = new Vector2(Width / 2, Height / 2);
        }
    }
}
