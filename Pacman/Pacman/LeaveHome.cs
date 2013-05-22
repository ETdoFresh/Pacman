using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class LeaveHome : IDisposable
    {
        private Ghost ghost;

        public delegate void FinishLeavingHandler(Ghost ghost);
        public event FinishLeavingHandler FinishLeaving = delegate { };

        public LeaveHome(Ghost ghost)
        {
            this.ghost = ghost;
            ghost.Target.Position.Value = TileEngine.GetPosition(13.5f, 14).Value;
            ghost.Steering.ArrivedAtTarget += NextTarget;
        }

        private void NextTarget()
        {
            ghost.Target.Position.Value = TileEngine.GetPosition(13.5f, 11).Value;
            ghost.Steering.ArrivedAtTarget -= NextTarget;
            ghost.Steering.ArrivedAtTarget += OnFinishLeaving;
        }

        private void OnFinishLeaving()
        {
            FinishLeaving(ghost);
        }

        public void Dispose()
        {
            ghost.Steering.ArrivedAtTarget -= NextTarget;
            ghost.Steering.ArrivedAtTarget -= OnFinishLeaving;
        }
    }
}
