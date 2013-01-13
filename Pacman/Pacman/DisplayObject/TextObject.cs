using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman.DisplayObject
{
    class TextObject : DisplayObject
    {
        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                var measurements = display.font.MeasureString(text);
                Width = measurements.X;
                Height = measurements.Y;
                XOrigin = Width / 2;
                YOrigin = Height / 2;
            }
        }

        public TextObject(string text)
        {
            Text = text;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(
                spriteFont:display.font,
                text: text,
                position:Position,
                color:Color.White,
                rotation:ContentOrientation,
                origin:new Vector2(XOrigin, YOrigin),
                scale: new Vector2(ContentXScale, ContentYScale),
                effects: SpriteEffects.None,
                layerDepth:0);
        }
    }
}
