using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman.DisplayEngine
{
    class RectangleObject : DisplayObject
    {
        private Texture2D texture;
        private Rectangle rectangle;
        private GraphicsDevice graphicsDevice = display.graphics.GraphicsDevice;


        public RectangleObject()
            : base()
        {
            rectangle = Rectangle.Empty;
            texture = new Texture2D(graphicsDevice, 1, 1);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (rectangle.Width != Width || rectangle.Height != Height)
            {
                rectangle = new Rectangle(0,0,(int)Width,(int)Height);
                texture = new Texture2D(graphicsDevice, rectangle.Width, rectangle.Height);
                var data = new Color[(int)(Width * Height)];
                for(int i = 0; i< data.Length; i++)
                    data[i] = Color.White;
                texture.SetData(data);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (IsVisible)
            {
                spriteBatch.Draw(
                    texture: texture,
                    position: new Vector2(ContentX, ContentY),
                    sourceRectangle: rectangle,
                    color: Color * Alpha,
                    rotation: ContentOrientation,
                    origin: new Vector2(XOrigin, YOrigin),
                    scale: new Vector2(ContentXScale, ContentYScale),
                    effects: SpriteEffects.None,
                    layerDepth: 0f);
            }
        }
    }
}
