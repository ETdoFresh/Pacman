using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class TunnelSpeed : IDisposable
    {
        private Speed speed;
        private TilePosition tilePosition;
        private float ghostTunnelSpeedFactor;
        private float previousSpeedFactor;

        public TunnelSpeed(Speed speed, TilePosition tilePosition, float ghostTunnelSpeedFactor)
        {
            this.speed = speed;
            this.tilePosition = tilePosition;
            this.ghostTunnelSpeedFactor = ghostTunnelSpeedFactor;
            previousSpeedFactor = speed.Factor;
            tilePosition.ChangeTile += OnTileChange;
        }

        private void OnTileChange()
        {
            if (speed.Factor != ghostTunnelSpeedFactor)
            {
                previousSpeedFactor = speed.Factor;
                if (tilePosition.Y == 14 && (tilePosition.X <= 5 || tilePosition.X >= 22))
                    speed.Factor = ghostTunnelSpeedFactor;
            }
            else
            {
                if (tilePosition.Y != 14 || (tilePosition.X > 5 && tilePosition.X < 22))
                    speed.Factor = previousSpeedFactor;
            }
        }

        public void Dispose()
        {
            tilePosition.ChangeTile -= OnTileChange;
        }
    }
}
