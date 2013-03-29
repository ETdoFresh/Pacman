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

        public static float GetXCoordinates(Int32 tileX)
        {
            return tileX * TileWidth + TileWidth / 2;
        }

        public static float GetYCoordinates(Int32 tileY)
        {
            return tileY * TileHeight + TileHeight / 2;
        }

        public static Position GetPosition(Int32 tileX, Int32 tileY)
        {
            return new Position(GetXCoordinates(tileX), GetYCoordinates(tileY));
        }
    }

    class TilePosition
    {
        private Position Position { get; set; }

        public int X { get { return (int)Math.Floor(Position.X / TileEngine.TileWidth); } }
        public int Y { get { return (int)Math.Floor(Position.Y / TileEngine.TileHeight); } }
        public Vector2 Value { get { return new Vector2(X, Y); } }

        public TilePosition(Position position)
        {
            Position = position;
        }

        public TilePosition Copy()
        {
            return new TilePosition(new Position(Position.Value));
        }
    }
}
