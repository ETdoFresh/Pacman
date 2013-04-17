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

        public static Position GetPosition(Vector2 tile)
        {
            return new Position(GetXCoordinates((int)Math.Floor(tile.X)), GetYCoordinates((int)Math.Floor(tile.Y)));
        }
    }

    class TilePosition : IDisposable
    {
        private Position position;
        private Position oldPosition;

        public Vector2 Value { get; private set; }
        public int X { get { return (int)Value.X; } }
        public int Y { get { return (int)Value.Y; } }

        public TilePosition(Position position)
        {
            this.position = position;
            oldPosition = position.Copy();
            Runtime.GameUpdate += UpdateTilePosition;
        }

        public void UpdateTilePosition(GameTime gameTime)
        {
            if (position.Value != oldPosition.Value)
            {
                var x = (int)Math.Floor(position.X / TileEngine.TileWidth);
                var y = (int)Math.Floor(position.Y / TileEngine.TileHeight);
                Value = new Vector2(x, y);
                oldPosition = position.Copy();
            }
        }

        public void Dispose()
        {
            Runtime.GameUpdate -= UpdateTilePosition;
        }

        public TilePosition Copy()
        {
            return new TilePosition(position.Copy());
        }
    }
}
