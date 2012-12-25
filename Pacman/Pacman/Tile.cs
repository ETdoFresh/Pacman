using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class Tile : Image
    {
        public Rectangle BoundingBox { get { return new Rectangle((int)X - Width / 2, (int)Y - Height / 2, Width, Height); } }

        public Tile(Texture2D texture, List<Rectangle> sourceRectangles, int tileIndex)
            : base(texture, sourceRectangles, tileIndex) { }

        public Tile(Texture2D texture) : base(texture) { }
    }
}
