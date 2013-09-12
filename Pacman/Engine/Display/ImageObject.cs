using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman.Engine.Display
{
    /// <summary>
    /// DisplayObject that displays an entire image
    /// </summary>
    class ImageObject : DisplayObject
    {
        private readonly string _assetFile;
        protected Texture2D _texture;
        protected Rectangle _sourceRectangle;

        public ImageObject(string assetFile) : base()
        {
            _assetFile = assetFile;
        }

        /// <summary>
        /// Loads an image file and set origin to center.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            _texture = Stage.GameContent.Load<Texture2D>(_assetFile);
            _sourceRectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);

            Width = _sourceRectangle.Width;
            Height = _sourceRectangle.Height;
            Origin = new Vector2(Width / 2, Height / 2);
        }

        /// <summary>
        /// Draw the image.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            if (Visible) 
                Stage.SpriteBatch.Draw(_texture, ContentPosition, _sourceRectangle, Tint * Alpha, ContentOrientation, Origin, ContentScale, SpriteEffects.None, 0);
        }
    }
}
