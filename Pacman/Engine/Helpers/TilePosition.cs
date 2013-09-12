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
        Vector2 _oldPosition;
        Vector2 _oldTile;
        Position _position;

        public int X { get; set; }
        public int Y { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public Vector2 Vector { get { return new Vector2(X, Y); } }

        public delegate void ChangeTileHandler();
        public event ChangeTileHandler ChangeTile = delegate { };

        public TilePosition(Position position, int tileWidth, int tileHeight)
        {
            _oldPosition = new Vector2(-123, -123);
            _position = position;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            UpdateTilePosition();
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                base.Update(gameTime);
                if (_position.Value != _oldPosition)
                {
                    UpdateTilePosition();

                    if (Vector != _oldTile)
                    {
                        ChangeTile();
                        _oldTile = Vector;
                    }
                }
            }
        }

        public override void RemoveSelf()
        {
            X = Y = -1;
            TileWidth = TileHeight = -1;
            ChangeTile = null;
            base.RemoveSelf();
        }

        private void UpdateTilePosition()
        {
            X = (int)Math.Floor(_position.X / TileWidth);
            Y = (int)Math.Floor(_position.Y / TileHeight);
            _oldPosition = _position.Value;
        }
    }
}
