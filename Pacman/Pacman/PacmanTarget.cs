using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class PacmanTarget : Target
    {
        private Direction lastSuccessfulDirection;
        private Direction lastDirection;
        private Tile[,] tiles;

        public PacmanTarget(GameObject source, Direction direction, Tile[,] tiles, GroupObject displayParent = null)
            : base(source, displayParent)
        {
            this.tiles = tiles;
            
            lastSuccessfulDirection = direction;
            lastDirection = new Direction(Direction.Left);

            rectangleGraphic.Color = Color.Yellow;
            rectangleGraphic.Alpha = 0.15f;

            KeyboardListener.Press += UpdateLastKey;
            Runtime.GameUpdate += UpdatePacmanTarget;
        }

        private void UpdateLastKey(Keys key)
        {
            if (key == Keys.Right || key == Keys.Left || key == Keys.Up || key == Keys.Down)
                lastDirection.Key = key;
        }

        private void UpdatePacmanTarget(GameTime gameTime)
        {
            var tilePosition = source.TilePosition.Value;
            if (tiles != null)
            {
                var newPosition = tilePosition + lastDirection.GetVectorOffset();
                if (0 <= newPosition.X && newPosition.X < tiles.GetLength(0))
                {
                    if (0 <= newPosition.Y && newPosition.Y < tiles.GetLength(1))
                    {
                        if (tiles[(int)newPosition.X, (int)newPosition.Y] == null || tiles[(int)newPosition.X, (int)newPosition.Y].IsPassable)
                        {
                            lastSuccessfulDirection.Value = lastDirection.Value;
                            Position.Value = TileEngine.GetPosition(newPosition).Value;
                            return;
                        }
                    }
                }
                newPosition = tilePosition + lastSuccessfulDirection.GetVectorOffset();
                if (0 <= newPosition.X && newPosition.X < tiles.GetLength(0))
                {
                    if (0 <= newPosition.Y && newPosition.Y < tiles.GetLength(1))
                    {
                        if (tiles[(int)newPosition.X, (int)newPosition.Y] == null || tiles[(int)newPosition.X, (int)newPosition.Y].IsPassable)
                        {
                            Position.Value = TileEngine.GetPosition(newPosition).Value;
                            return;
                        }
                    }
                }

                if ((newPosition.X == -1 || newPosition.X == tiles.GetLength(0)) && newPosition.Y == 14)
                {
                    Position.Value = TileEngine.GetPosition(newPosition).Value;
                    return;
                }
            }
        }

        public override void Dispose()
        {
            KeyboardListener.Press -= UpdateLastKey;
            Runtime.GameUpdate -= UpdatePacmanTarget;
            base.Dispose();
        }
    }
}
