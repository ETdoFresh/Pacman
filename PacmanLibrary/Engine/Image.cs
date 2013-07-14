using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PacmanLibrary.Engine
{
    class Image : DisplayObject
    {
        private readonly string _assetFile;
        protected Texture2D _texture;

        public Image(string assetFile, Group parent = null)
            : base(parent)
        {
            _assetFile = assetFile;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            _texture = Content.Load<Texture2D>(_assetFile);
        }

        public override void Draw(RenderContext renderContext)
        {
            // Do not draw DisplayObject
            // base.Draw(renderContext);

            if (Visible)
                renderContext.SpriteBatch.Draw(_texture, ContentPosition, Color);
        }
    }
}
