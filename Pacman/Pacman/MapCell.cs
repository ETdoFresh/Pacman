using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Pacman
{
    class MapCell
    {
        private List<Tile> tiles = new List<Tile>();

        public bool IsPassable { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public MapCell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void addLayer(Tile tile)
        {
            tiles.Add(tile);
        }

        public void removeLayer(Tile tile)
        {
            tiles.Remove(tile);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in tiles)
                tile.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
