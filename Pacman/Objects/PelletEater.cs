using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Microsoft.Xna.Framework;
using Pacman.Engine.Helpers;

namespace Pacman.Objects
{
    class PelletEater : GameObject
    {
        PacmanObject _pacman;
        Pellet[,] _pelletGrid;

        public PelletEater(PacmanObject pacman, List<Pellet> pellets, TileGrid tileGrid)
        {
            _pacman = pacman;
            _pelletGrid = new Pellet[tileGrid.NumberOfXTiles, tileGrid.NumberOfYTiles];

            foreach (var pellet in pellets)
                _pelletGrid[pellet.TilePosition.X, pellet.TilePosition.Y] = pellet;

            _pacman.TilePosition.ChangeTile += OnChangeTile;
        }

        public override void RemoveSelf()
        {
            _pacman.TilePosition.ChangeTile -= OnChangeTile;
            base.RemoveSelf();
        }

        private void OnChangeTile()
        {
            if (0 <= _pacman.TilePosition.X && _pacman.TilePosition.X < _pelletGrid.GetLength(0) &&
                0 <= _pacman.TilePosition.Y && _pacman.TilePosition.Y < _pelletGrid.GetLength(1) &&
                _pelletGrid[_pacman.TilePosition.X, _pacman.TilePosition.Y] != null)
            {
                var pellet = _pelletGrid[_pacman.TilePosition.X, _pacman.TilePosition.Y];
                pellet.RemoveSelf();
                _pelletGrid[_pacman.TilePosition.X, _pacman.TilePosition.Y] = null;
                _pacman.Speed.Factor = 0.5f;
            }
            else
            {
                _pacman.Speed.Factor = 1;
            }
        }
    }
}
