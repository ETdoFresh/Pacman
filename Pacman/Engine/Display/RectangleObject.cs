using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman.Engine.Display
{
    /// <summary>
    /// DisplayObject in the shape of a square/rectangle.
    /// </summary>
    class RectangleObject : DisplayObject
    {
        static List<Texture2D> _previousTextures;

        Rectangle _rectangle;
        Texture2D _texture;

        static RectangleObject()
        {
            _previousTextures = new List<Texture2D>();
        }

        public RectangleObject(float width, float height)
            : base()
        {
            _rectangle = new Rectangle();
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Create/Load a Rectangle Texture. Loads previous rectangle if rectangle of the same size has been created before.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            
            if (!FindPreviousTexture())
            {
                CreateNewRectangleTexture();
                _previousTextures.Add(_texture);
            }

            Origin = new Vector2(Width / 2, Height / 2);
        }

        /// <summary>
        /// Sets up rectangle to be drawn
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
        /// Draws rectangle on screen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            Stage.SpriteBatch.Draw(_texture, _rectangle, null, Tint * Alpha, ContentOrientation, Origin, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Finds previous texture if rectangle is same size has been created.
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
        /// Creates a new rectangle texture.
        /// </summary>
        private void CreateNewRectangleTexture()
        {
            _texture = new Texture2D(Stage.GameGraphicsDevice, (int)Width, (int)Height);
            Color[] color = new Color[(int)(Width * Height)];
            for (int i = 0; i < color.Length; i++) color[i] = Color.White;
            _texture.SetData(color);
        }
    }
}
