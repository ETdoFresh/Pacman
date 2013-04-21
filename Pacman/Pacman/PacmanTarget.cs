using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    abstract class Target : GameObject
    {
        protected RectangleObject rectangleGraphic;
        protected GameObject source;

        public Target(GameObject source, GroupObject displayParent = null)
        {
            this.source = source;
            Position = new Position();
            TilePosition = new TilePosition(Position);

            var dimension = new Dimension(TileEngine.TileWidth, TileEngine.TileHeight);
            rectangleGraphic = new RectangleObject(parent: displayParent, position: Position, dimension: dimension);
            disposables.Add(rectangleGraphic);
        }
    }


    class PacmanTarget : Target
    {
        private Keys lastSuccessfulKey = Keys.Left;
        private Keys lastKey = Keys.Left;
        private Tile[,] tiles;

        public PacmanTarget(GameObject source, Tile[,] tiles, GroupObject displayParent = null)
            : base(source, displayParent)
        {
            this.tiles = tiles;

            rectangleGraphic.Color = Color.Yellow;
            rectangleGraphic.Alpha = 0.15f;

            KeyboardListener.Press += UpdateLastKey;
            Runtime.GameUpdate += UpdatePacmanTarget;
        }

        private void UpdateLastKey(Keys key)
        {
            if (key == Keys.Right || key == Keys.Left || key == Keys.Up || key == Keys.Down)
                lastKey = key;
        }

        private void UpdatePacmanTarget(GameTime gameTime)
        {
            var tilePosition = source.TilePosition.Value;
            if (tiles != null)
            {
                var newPosition = tilePosition + GetTileOffsetFromKey(lastKey);
                if (0 <= newPosition.X && newPosition.X < tiles.GetLength(0))
                {
                    if (0 <= newPosition.Y && newPosition.Y < tiles.GetLength(1))
                    {
                        if (tiles[(int)newPosition.X, (int)newPosition.Y] == null || tiles[(int)newPosition.X, (int)newPosition.Y].IsPassable)
                        {
                            lastSuccessfulKey = lastKey;
                            Position.Value = TileEngine.GetPosition(newPosition).Value;
                            return;
                        }
                    }
                }
                newPosition = tilePosition + GetTileOffsetFromKey(lastSuccessfulKey);
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

        private Vector2 GetTileOffsetFromKey(Keys key)
        {
            var offset = Vector2.Zero;

            if (key == Keys.Up)
                offset.Y--;
            else if (key == Keys.Down)
                offset.Y++;
            else if (key == Keys.Left)
                offset.X--;
            else if (key == Keys.Right)
                offset.X++;

            return offset;
        }

        public override void Dispose()
        {
            KeyboardListener.Press -= UpdateLastKey;
            Runtime.GameUpdate -= UpdatePacmanTarget;
            base.Dispose();
        }
    }
}
