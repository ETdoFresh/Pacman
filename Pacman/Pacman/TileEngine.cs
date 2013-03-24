using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;

namespace Pacman
{
    class TileEngine
    {
        public static float TileWidth { get; set; }
        public static float TileHeight { get; set; }

        public static void Initialize(String filename, Int32 index)
        {
            var sprite = new Sprite(filename: filename, index: index);
            TileWidth = sprite.Width;
            TileHeight = sprite.Height;
            sprite.Dispose();
        }

        public static float GetXCoordinates(Int32 tileX)
        {
            return tileX * TileWidth + TileWidth / 2;
        }

        public static float GetYCoordinates(Int32 tileY)
        {
            return tileY * TileHeight + TileHeight / 2;
        }
    }
}
