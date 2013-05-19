using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class LeaveHome : IDisposable
    {
        private Ghost ghost;

        public LeaveHome(Ghost ghost)
        {
            this.ghost = ghost;
            ghost.Target.Position.Value = TileEngine.GetPosition(13.5f, 14).Value;
            ghost.Steering.ArrivedAtTarget += NextTarget;
        }

        void NextTarget(object sender, EventArgs e)
        {
            ghost.Target.Position.Value = TileEngine.GetPosition(13.5f, 11).Value;
            ghost.Steering.ArrivedAtTarget -= NextTarget;
        }

        public void Dispose()
        {
            ghost.Steering.ArrivedAtTarget -= NextTarget;
        }
    }
}
