using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Microsoft.Xna.Framework;

namespace Pacman.Engine.Helpers
{
    class TilePosition : GameObject
    {
        Vector2 _oldValue;
        Position _position;

        public int X { get; set; }
        public int Y { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public Vector2 Vector { get { return new Vector2(X, Y); } }

        public TilePosition(Position position, int tileWidth, int tileHeight)
        {
            _oldValue = new Vector2(-123, -123);
            _position = position;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            UpdateTilePosition();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (_position.Value != _oldValue)
            {
                UpdateTilePosition();
            }
        }

        private void UpdateTilePosition()
        {
            _oldValue = _position.Value;
            X = (int)Math.Floor(_position.X / TileWidth);
            Y = (int)Math.Floor(_position.Y / TileHeight);
        }

        public Vector2 VectorPositionFromTile(float tileX, float tileY)
        {
            var posX = tileX * TileWidth + TileWidth / 2;
            var posY = tileY * TileHeight + TileHeight / 2;
            return new Vector2(posX, posY);
        }

        public Vector2 VectorPositionFromTile(Vector2 tile)
        { return VectorPositionFromTile(tile.X, tile.Y); }
    }
}
