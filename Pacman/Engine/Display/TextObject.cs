using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman.Engine.Display
{
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

        public override void LoadContent()
        {
            base.LoadContent();
            _spriteFont = Content.Load<SpriteFont>("SpriteFont");
            ChangeText("");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_newText != null && _newText != _text)
            {
                ChangeText(_newText);
                _newText = null;
            }
        }

        private void ChangeText(string newText)
        {
            _text = newText;
            var measureFont = _spriteFont.MeasureString(_text);
            Width = measureFont.X;
            Height = measureFont.Y;
            Origin = new Vector2(Width / 2, Height / 2);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.DrawString(_spriteFont, _text, ContentPosition, Tint * Alpha, ContentOrientation, Origin, ContentScale, SpriteEffects.None, 0);
        }

    }
}
