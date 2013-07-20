using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman.Engine
{
    class ImageObject : DisplayObject
    {
        private readonly string _assetFile;
        protected Texture2D _texture;

        public ImageObject(string assetFile) : base()
        {
            _assetFile = assetFile;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _texture = Content.Load<Texture2D>(_assetFile);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(_texture, ContentPosition, Tint * Alpha);
        }
    }
}
