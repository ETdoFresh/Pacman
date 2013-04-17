using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DisplayLibrary
{
    public class RectangleObject : DisplayObject
    {
        private Rectangle rectangle = new Rectangle();
        

        public RectangleObject(GroupObject parent = null, Position position = null, Dimension dimension = null, Rotation rotation = null, Scale scale = null)
            : base(parent, position, dimension, rotation, scale)
        {
            texture = new Texture2D(ContentLoader.GraphicsDevice, 1, 1);
            texture.SetData(new Color[1] { Color.White });

            Runtime.GameUpdate += UpdateRectangle;
        }

        private void UpdateRectangle(GameTime gameTime)
        {
            var rectX = ContentPosition.X - Dimension.Width / 2;
            var rectY = ContentPosition.Y - Dimension.Height / 2;
            rectangle = new Rectangle((int)rectX, (int)rectY, (int)Dimension.Width, (int)Dimension.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, null, Color * Alpha, 0, Vector2.Zero, SpriteEffects.None, 0);
        }

        public override void Dispose()
        {
            Runtime.GameUpdate -= UpdateRectangle;
            base.Dispose();
        }
    }
}
