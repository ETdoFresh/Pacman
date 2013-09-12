using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Helpers;
using Pacman.Engine.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pacman.Objects
{
    class PlayerMovement : GameObject
    {
        private PacmanObject _pacman;
        private Target _target;
        private TileGrid _tileGrid;
        private Vector2 _pacmanPreviousPosition;

        public PlayerMovement(PacmanObject pacman, Target target, TileGrid tileGrid)
        {
            _pacman = pacman;
            _target = target;
            _tileGrid = tileGrid;
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                base.Update(gameTime);
                SetNewDirection();
                SetNewTarget();
            }
        }

        private void SetNewDirection()
        {
            if (InputHelper.IsPressed(Keys.Left))
                _pacman.DesiredDirection.Value = Direction.LEFT;
            else if (InputHelper.IsPressed(Keys.Right))
                _pacman.DesiredDirection.Value = Direction.RIGHT;
            else if (InputHelper.IsPressed(Keys.Up))
                _pacman.DesiredDirection.Value = Direction.UP;
            else if (InputHelper.IsPressed(Keys.Down))
                _pacman.DesiredDirection.Value = Direction.DOWN;
        }

        private void SetNewTarget()
        {
            var pacmanPosition = _pacman.TilePosition.Vector;
            var pacmanDesiredDirection = _pacman.DesiredDirection.Offset;
            var pacmanPreviousDirection = _pacman.PreviousDirection.Offset;

            if (pacmanPosition != _pacmanPreviousPosition || pacmanDesiredDirection != pacmanPreviousDirection)
            {
                _pacmanPreviousPosition = pacmanPosition;

                var pacmanNewPosition = pacmanPosition + pacmanDesiredDirection;
                if (pacmanNewPosition.X < 0 || _tileGrid.Data.GetLength(0) <= pacmanNewPosition.X ||
                    pacmanNewPosition.Y < 0 || _tileGrid.Data.GetLength(1) <= pacmanNewPosition.Y ||
                    _tileGrid.Data[(int)pacmanNewPosition.X, (int)pacmanNewPosition.Y].IsPassable)
                {
                    pacmanNewPosition.X = pacmanNewPosition.X * _tileGrid.TileWidth + _tileGrid.TileWidth / 2;
                    pacmanNewPosition.Y = pacmanNewPosition.Y * _tileGrid.TileHeight + _tileGrid.TileHeight / 2;
                    _target.Position.Value = pacmanNewPosition;
                    _pacman.PreviousDirection.Value = _pacman.DesiredDirection.Value;
                    return;
                }

                pacmanNewPosition = pacmanPosition + pacmanPreviousDirection;
                if (pacmanNewPosition.X < 0 || _tileGrid.Data.GetLength(0) <= pacmanNewPosition.X ||
                    pacmanNewPosition.Y < 0 || _tileGrid.Data.GetLength(1) <= pacmanNewPosition.Y ||
                    _tileGrid.Data[(int)pacmanNewPosition.X, (int)pacmanNewPosition.Y].IsPassable)
                {
                    pacmanNewPosition.X = pacmanNewPosition.X * _tileGrid.TileWidth + _tileGrid.TileWidth / 2;
                    pacmanNewPosition.Y = pacmanNewPosition.Y * _tileGrid.TileHeight + _tileGrid.TileHeight / 2;
                    _target.Position.Value = pacmanNewPosition;
                }
            }
        }
    }
}
