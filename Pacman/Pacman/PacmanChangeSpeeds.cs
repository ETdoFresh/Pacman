using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class PacmanChangeSpeeds : IDisposable
    {
        private Speed speed;
        private TilePosition tilePosition;
        private float pacmanSpeedFactor;

        public PacmanChangeSpeeds(Speed speed, TilePosition tilePosition, float pacmanSpeedFactor)
        {
            this.speed = speed;
            this.tilePosition = tilePosition;
            this.pacmanSpeedFactor = pacmanSpeedFactor;

            tilePosition.ChangeTile += OnChangeTile;
        }

        private void OnChangeTile()
        {
            speed.Factor = pacmanSpeedFactor;
        }

        public void Dispose()
        {
            tilePosition.ChangeTile -= OnChangeTile;
        }
    }
}
