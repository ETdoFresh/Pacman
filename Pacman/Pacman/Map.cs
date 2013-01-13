using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class Map
    {
        private Texture2D texture;
        private List<Rectangle> textureRectangles;

        public Map(Texture2D texture, List<Rectangle> textureRectangles)
        {
            this.texture = texture;
            this.textureRectangles = textureRectangles;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
