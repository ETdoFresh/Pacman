using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman.Engine.Display
{
    /// <summary>
    /// DisplayObject in the shape of a Circle
    /// </summary>
    class CircleObject : DisplayObject
    {
        static List<Texture2D> _previousTextures;

        Rectangle _rectangle;
        Texture2D _texture;

        static CircleObject()
        {
            _previousTextures = new List<Texture2D>();
        }

        public CircleObject(float radius)
            : base()
        {
            _rectangle = new Rectangle();
            Width = radius * 2;
            Height = radius * 2;
        }

        /// <summary>
        /// Create/Load a Circle Texture. Loads previous circle if a circle of the same radius has been created before.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            
            if (!FindPreviousTexture())
            {
                CreateNewCircleTexture();
                _previousTextures.Add(_texture);
            }

            Origin = new Vector2(Width / 2, Height / 2);
        }

        /// <summary>
        /// Sets up a rectangle to draw circle when Draw is called.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var width = (int)(Width * ContentScale);
            var height = (int)(Height * ContentScale);
            _rectangle = new Rectangle((int)ContentPosition.X, (int)ContentPosition.Y, width, height);
        }

        /// <summary>
        /// Draw the circle
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            Stage.SpriteBatch.Draw(_texture, _rectangle, null, Tint * Alpha, ContentOrientation, Origin, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Finds previous texture if circle of same radius has been loaded.
        /// </summary>
        /// <returns></returns>
        private bool FindPreviousTexture()
        {
            if (_previousTextures != null)
            {
                foreach (var texture in _previousTextures)
                {
                    if (texture.Width == Width && texture.Height == Height)
                    {
                        _texture = texture;
                        return true;
                    }
                }
            }
            return false;
        }
        
        /// <summary>
        /// Create a new circle texture by creating a square texture, and making edges transparent.
        /// </summary>
        private void CreateNewCircleTexture()
        {
            _texture = new Texture2D(Stage.GameGraphicsDevice, (int)Width, (int)Height);
            var radius = Width / 2;
            Color[] color = new Color[(int)(Width * Height)];
            for (int i = 0; i < color.Length; i++)
            {
                int x = (i + 1) % (int)Width;
                int y = (i + 1) / (int)Width;
                Vector2 distance = new Vector2(Math.Abs(radius - x), Math.Abs(radius - y));
                if (Math.Sqrt((distance.X * distance.X) + (distance.Y * distance.Y)) > radius)
                    color[i] = Color.Black * 0;
                else
                    color[i] = Color.White;
            }
            _texture.SetData(color);
        }
    }
}
