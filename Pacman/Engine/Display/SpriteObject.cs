using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman.Engine.Display
{
    class SpriteObject : ImageObject
    {
        protected readonly string _sheetDefinition;
        private int _index;

        protected List<Rectangle> _sourceRectangles;


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

            Width = _sourceRectangle.Width;
            Height = _sourceRectangle.Height;
            Origin = new Vector2(Width / 2, Height / 2);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(_texture, ContentPosition, _sourceRectangle, Tint * Alpha, ContentRotation, Origin, ContentScale, SpriteEffects.None, 0);
        }
    }
}
