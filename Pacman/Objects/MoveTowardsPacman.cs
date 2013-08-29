using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Helpers;
using Pacman.Engine.Display;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class MoveTowardsPacman : GameObject
    {
        private Pacman _pacman;
        private Ghost _ghost;
        private TileGrid _tileGrid;

        public MoveTowardsPacman(Ghost ghost, Pacman pacman, TileGrid tileGrid)
        {
            _pacman = pacman;
            _ghost = ghost;
            _tileGrid = tileGrid;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (possibleFutureOptions() > 1)
            {
                System.Diagnostics.Debug.WriteLine("Intersection");
            }
        }

        private int possibleFutureOptions()
        {
            var numberOfFutureOptions = 0;
            var futurePosition = _ghost.Position.Value + _ghost.Direction.Offset;
            var futureOptions = new Vector2[] 
            {
                new Vector2(futurePosition.X - 1, futurePosition.Y),
                new Vector2(futurePosition.X + 1, futurePosition.Y),
                new Vector2(futurePosition.X, futurePosition.Y - 1),
                new Vector2(futurePosition.X, futurePosition.Y + 1),
            };

            foreach (var option in futureOptions)
            {
                if (option != _pacman.Position.Value && 0 <= option.X && option.X <= _tileGrid.Width
                    && _tileGrid.Data[(int)option.X, (int)option.Y].IsPassable)
                {
                    numberOfFutureOptions++;
                }
            }

            return numberOfFutureOptions;
        }
    }
}
