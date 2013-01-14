using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman.DisplayEngine
{
    class ImageObject : DisplayObject
    {
        private ImageSheet imageSheet;

        public ImageObject(Texture2D texture)
            : base()
        {
            var rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            imageSheet = new ImageSheet() { texture = texture, sourceRectangle = rectangle };

            Width = texture.Width;
            Height = texture.Height;
            XOrigin = Width / 2;
            YOrigin = Height / 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (IsVisible)
            {
                spriteBatch.Draw(
                    texture: imageSheet.texture,
                    position: new Vector2(ContentX, ContentY),
                    sourceRectangle: imageSheet.sourceRectangle,
                    color: Color * Alpha,
                    rotation: ContentOrientation,
                    origin: new Vector2(XOrigin, YOrigin),
                    scale: new Vector2(ContentXScale, ContentYScale),
                    effects: SpriteEffects.None,
                    layerDepth: 0f);
            }
        }
    }

    class ImageSheet
    {
        public Texture2D texture;
        public Rectangle sourceRectangle;
        public List<Rectangle> sourceRectangles;
    }
}
