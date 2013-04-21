using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class TileEngine
    {
        public static float TileWidth { get; set; }
        public static float TileHeight { get; set; }

        public static void Initialize(String filename, Int32 index)
        {
            var sprite = new Sprite(filename: filename, index: index);
            TileWidth = sprite.Dimension.Width;
            TileHeight = sprite.Dimension.Height;
            sprite.Dispose();
        }

        public static float GetXCoordinates(float tileX)
        {
            return tileX * TileWidth + TileWidth / 2;
        }

        public static float GetYCoordinates(float tileY)
        {
            return tileY * TileHeight + TileHeight / 2;
        }

        public static Position GetPosition(float tileX, float tileY)
        {
            return new Position(GetXCoordinates(tileX), GetYCoordinates(tileY));
        }

        public static Dimension GetDimension()
        {
            return new Dimension(TileWidth, TileHeight);
        }

        public static Position GetPosition(Vector2 tile)
        {
            return new Position(GetXCoordinates((int)Math.Floor(tile.X)), GetYCoordinates((int)Math.Floor(tile.Y)));
        }
    }
}
