using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class NextTile : GameObject
    {
        private Ghost ghost;
        private RectangleObject rectangle;
        private Tile[,] tiles;
        private Direction direction;

        public NextTile(Ghost ghost, Direction direction, Tile[,] tiles, GroupObject displayParent)
        {
            this.ghost = ghost;
            this.direction = direction;
            this.tiles = tiles;
            Position = new Position();
            TilePosition = new TilePosition(Position);

            rectangle = new RectangleObject(displayParent, Position, TileEngine.GetDimension());
            rectangle.Color = Color.White;
            rectangle.Alpha = 0.4f;

            UpdateNextTile();
        }

        public void UpdateNextTile()
        {
            var currentTile = ghost.TilePosition.Value;
            if (tiles != null)
            {
                var newPosition = currentTile + direction.GetVectorOffset();
                Position.Value = TileEngine.GetPosition(newPosition).Value;
            }
        }
    }
}
