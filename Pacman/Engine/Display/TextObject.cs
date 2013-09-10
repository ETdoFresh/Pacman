using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman.Engine.Display
{
    /// <summary>
    /// DisplayObject that displays text
    /// </summary>
    class TextObject : DisplayObject
    {
        SpriteFont _spriteFont;
        string _text;
        string _newText;

        public string Text { get { return _text; } set { _newText = value; } }

        public TextObject(String text)
            : base()
        {
            _newText = text;
        }

        /// <summary>
        /// Loads text object and initialzes to an empty string
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
            _spriteFont = Stage.GameContent.Load<SpriteFont>("SpriteFont");
            ChangeText("");
        }

        /// <summary>
        /// If text has changed, update to new text.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_newText != null && _newText != _text)
            {
                ChangeText(_newText);
                _newText = null;
            }
        }

        /// <summary>
        /// Draws text to screen
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            Stage.SpriteBatch.DrawString(_spriteFont, _text, ContentPosition, Tint * Alpha, ContentOrientation, Origin, ContentScale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Sets up a new text object and sets origin to center.
        /// </summary>
        /// <param name="newText"></param>
        private void ChangeText(string newText)
        {
            _text = newText;
            var measureFont = _spriteFont.MeasureString(_text);
            Width = measureFont.X;
            Height = measureFont.Y;
            Origin = new Vector2(Width / 2, Height / 2);
        }
    }
}
