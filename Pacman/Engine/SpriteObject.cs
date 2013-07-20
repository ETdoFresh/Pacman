using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman.Engine
{
    class SpriteObject : ImageObject
    {
        protected readonly string _sheetDefinition;
        private int _index;

        protected List<Rectangle> _sourceRectangles;
        protected Rectangle _sourceRectangle;

        public SpriteObject(string assetFile, int index)
            : base(assetFile)
        {
            _sheetDefinition = assetFile + "xml";
            _index = index;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            _sourceRectangles = Content.Load<List<Rectangle>>(_sheetDefinition);
            _sourceRectangle = _sourceRectangles[_index];
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(_texture, Position, _sourceRectangle, Tint);
        }
    }
}
