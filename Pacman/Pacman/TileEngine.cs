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

    class TilePosition : IDisposable
    {
        private Position Position { get; set; }
        private Position OldPosition { get; set; }

        public Vector2 Value { get; set; }
        public int X { get { return (int)Value.X; } set { Value = new Vector2(value, Value.Y); } }
        public int Y { get { return (int)Value.Y; } set { Value = new Vector2(Value.X, value); } }

        public TilePosition(Position position)
        {
            Position = position;
            OldPosition = Position.Copy();
            Runtime.GameUpdate += UpdateTilePosition;
        }

        public void UpdateTilePosition(GameTime gameTime)
        {
            if (Position.Value != OldPosition.Value)
            {
                var x = (int)Math.Floor(Position.X / TileEngine.TileWidth);
                var y = (int)Math.Floor(Position.Y / TileEngine.TileHeight);
                Value = new Vector2(x, y);
                OldPosition = Position.Copy();
            }
        }

        public void Dispose()
        {
            Runtime.GameUpdate -= UpdateTilePosition;
        }

        public TilePosition Copy()
        {
            return new TilePosition(Position.Copy());
        }
    }
}
