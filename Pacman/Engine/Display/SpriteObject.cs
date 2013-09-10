using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman.Engine.Display
{
    /// <summary>
    /// Same as ImageObject, but displays a sprite/tile instead of entire image.
    /// </summary>
    class SpriteObject : ImageObject
    {
        protected readonly string _sheetDefinition;
        protected List<Rectangle> _sourceRectangles;
        int _index;

        public SpriteObject(string assetFile, int index)
            : base(assetFile)
        {
            _sheetDefinition = assetFile + "xml";
            _index = index;
        }

        /// <summary>
        /// Loads sprite from spritesheet created by superclass image and spritesheet definition.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            _sourceRectangles = Stage.GameContent.Load<List<Rectangle>>(_sheetDefinition);
            ChangeIndex(_index);
        }

        /// <summary>
        /// Changes the sprite and updates properties
        /// </summary>
        /// <param name="index">Sprite index</param>
        public void ChangeIndex(int index)
        {
            _index = index;
            if (_sourceRectangles != null)
            {
                _sourceRectangle = _sourceRectangles[_index];
                Width = _sourceRectangle.Width;
                Height = _sourceRectangle.Height;
                Origin = new Vector2(Width / 2, Height / 2);
            }
        }
    }
}
