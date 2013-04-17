using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class PacmanTarget : GameObject
    {
        private RectangleObject graphic;
        private Pacman pacman;
        private Keys lastSuccessfulKey = Keys.Left;
        private Keys lastKey = Keys.Left;

        public Tile[,] Tiles { get; set; }

        public PacmanTarget(Pacman pacman, GroupObject displayParent = null)
        {
            this.pacman = pacman;
            Position = TileEngine.GetPosition(1, 1);
            TilePosition = new TilePosition(Position);
            DisplayParent = displayParent;

            var dimension = new Dimension(TileEngine.TileWidth, TileEngine.TileHeight);
            graphic = new RectangleObject(parent: DisplayParent, position: Position, dimension: dimension);
            graphic.Color = Color.Yellow;
            graphic.Alpha = 0.15f;
            disposables.Add(graphic);

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
            var tilePosition = pacman.TilePosition.Value;
            if (Tiles != null)
            {
                var newPosition = tilePosition + GetTileOffsetFromKey(lastKey);
                if (0 <= newPosition.X && newPosition.X < Tiles.GetLength(0))
                {
                    if (0 <= newPosition.Y && newPosition.Y < Tiles.GetLength(1))
                    {
                        if (Tiles[(int)newPosition.X, (int)newPosition.Y] == null || Tiles[(int)newPosition.X, (int)newPosition.Y].IsPassable)
                        {
                            lastSuccessfulKey = lastKey;
                            Position.Value = TileEngine.GetPosition(newPosition).Value;
                            return;
                        }
                    }
                }
                newPosition = tilePosition + GetTileOffsetFromKey(lastSuccessfulKey);
                if (0 <= newPosition.X && newPosition.X < Tiles.GetLength(0))
                {
                    if (0 <= newPosition.Y && newPosition.Y < Tiles.GetLength(1))
                    {
                        if (Tiles[(int)newPosition.X, (int)newPosition.Y] == null || Tiles[(int)newPosition.X, (int)newPosition.Y].IsPassable)
                        {
                            Position.Value = TileEngine.GetPosition(newPosition).Value;
                            return;
                        }
                    }
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
