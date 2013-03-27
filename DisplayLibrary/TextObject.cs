using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DisplayLibrary
{
    public class TextObject : DisplayObject
    {
        private SpriteFont spriteFont;
        private String text;

        public String Text
        {
            get
            {
                return text;
            }
            set
            {
                if (value != text)
                {
                    text = value;
                    var measureFont = spriteFont.MeasureString(text);
                    Width = measureFont.X;
                    Height = measureFont.Y;
                    origin = new Vector2(Width / 2, Height / 2);
                }
            }
        }

        public TextObject(String text, GroupObject parent = null, Position position = null, Rotation rotation = null, Scale scale = null)
            : base(parent, position, rotation, scale)
        {
            spriteFont = ContentLoader.GetSpriteFont();
            Text = text;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, text, Position.Value, Color.White, Rotation.Value, origin, Scale.Value, SpriteEffects.None, 0);
        }

    }
}
