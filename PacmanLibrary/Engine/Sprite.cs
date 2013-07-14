using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PacmanLibrary.Engine
{
    class Sprite : Image
    {
        protected List<Rectangle> _sourceRectangles;
        protected Rectangle _sourceRectangle;

        private string _sheetDefinition;
        private int _index;

        public Sprite(string assetFile, int index, Group parent = null)
            : base(assetFile, parent)
        {
            _sheetDefinition = assetFile + "xml";
            _index = index;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            _sourceRectangles = Content.Load<List<Rectangle>>(_sheetDefinition);

            System.Diagnostics.Debug.Assert(_sourceRectangles.Count >= _index);
            _sourceRectangle = _sourceRectangles[_index];
        }

        public override void Draw(RenderContext renderContext)
        {
            // Do not draw Sprite
            // base.Draw(renderContext);

            if (Visible)
                renderContext.SpriteBatch.Draw(_texture, ContentPosition, _sourceRectangle, Color);
        }
    }
}
