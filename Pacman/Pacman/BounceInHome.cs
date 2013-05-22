using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class BounceInHome : IDisposable
    {
        private Ghost ghost;

        public BounceInHome(Ghost ghost)
        {
            this.ghost = ghost;
            ghost.Target.Position.Value = ghost.StartPosition.Value;
            ghost.Steering.ArrivedAtTarget += SwitchTarget;
        }

        void SwitchTarget()
        {
            if (ghost.Position.Value == ghost.StartPosition.Value)
            {
                ghost.Target.Position.Value = ghost.StartPosition2.Value;
                ghost.Direction.Value = ghost.StartPosition.Y > ghost.StartPosition2.Y ? Direction.Up : Direction.Down;
            }
            else
            {
                ghost.Target.Position.Value = ghost.StartPosition.Value;
                ghost.Direction.Value = ghost.StartPosition.Y > ghost.StartPosition2.Y ? Direction.Down : Direction.Up;
            }
        }

        public void Dispose()
        {
            ghost.Steering.ArrivedAtTarget -= SwitchTarget;
        }
    }
}
