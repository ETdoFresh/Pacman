using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class TilePosition : IDisposable
    {
        private Position position;
        private Position oldPosition;

        public Vector2 Value { get; private set; }
        public int X { get { return (int)Value.X; } }
        public int Y { get { return (int)Value.Y; } }

        public delegate void ChangeTileHandler();
        public event ChangeTileHandler ChangeTile = delegate { };

        public TilePosition(Position position)
        {
            this.position = position;
            oldPosition = new Position();
            UpdateTilePosition();
            Runtime.GameUpdate += UpdateTilePosition;
        }

        public void UpdateTilePosition(GameTime gameTime = null)
        {
            if (position.Value != oldPosition.Value)
            {
                var oldValue = Value;
                var x = (int)Math.Floor(position.X / TileEngine.TileWidth);
                var y = (int)Math.Floor(position.Y / TileEngine.TileHeight);
                Value = new Vector2(x, y);
                oldPosition = position.Copy();
                
                if (oldValue != Value)
                    ChangeTile();
            }
        }

        public void Dispose()
        {
            ChangeTile = null;
            Runtime.GameUpdate -= UpdateTilePosition;
        }

        public TilePosition Copy()
        {
            return new TilePosition(position.Copy());
        }
    }
}
