using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class GhostState
    {
        public enum GhostStates { Home, LeavingHome, Chase, Scatter, Frightened, FrightenedFlashing, Eyes }
        static public GhostStates HOME { get { return GhostStates.Home; } }
        static public GhostStates LEAVINGHOME { get { return GhostStates.LeavingHome; } }
        static public GhostStates CHASE { get { return GhostStates.Chase; } }
        static public GhostStates SCATTER { get { return GhostStates.Scatter; } }
        static public GhostStates FRIGHTENED { get { return GhostStates.Frightened; } }
        static public GhostStates FRIGHTENEDFLASHING { get { return GhostStates.FrightenedFlashing; } }
        static public GhostStates EYES { get { return GhostStates.Eyes; } }

        GhostStates _currentState;

        private GhostState(GhostStates currentState)
        {
            _currentState = currentState; 
        }

        static public GhostState Change(GhostStates ghostState, Ghost ghost)
        {
            switch (ghostState)
            {
                case GhostStates.Home:
                    return new Home(ghost);
                case GhostStates.LeavingHome:
                    return new LeavingHome(ghost);
                case GhostStates.Chase:
                    return new Chase(ghost);
                case GhostStates.Scatter:
                    return new Scatter(ghost);
                case GhostStates.Frightened:
                    return new Frightened(ghost);
                case GhostStates.FrightenedFlashing:
                    return new FrightenedFlashing(ghost);
                case GhostStates.Eyes:
                    return new Eyes(ghost);
                default:
                    throw new Exception("Ghost State not valid");
            }
        }

        public GhostStates CurrentState { get { return _currentState; } }

        private class Home : GhostState
        {
            public Home(Ghost ghost) : base(HOME) { ghost.OnHomeState(); }
        }

        private class LeavingHome : GhostState
        {
            public LeavingHome(Ghost ghost) : base(LEAVINGHOME) { ghost.OnLeavingHomeState(); }
        }

        private class Chase : GhostState
        {
            public Chase(Ghost ghost) : base(CHASE) { ghost.OnChaseState(); }
        }

        private class Scatter : GhostState
        {
            public Scatter(Ghost ghost) : base(SCATTER) { ghost.OnScatterState(); }
        }

        private class Frightened : GhostState
        {
            public Frightened(Ghost ghost) : base(FRIGHTENED) { ghost.OnFrightenedState(); }
        }

        private class FrightenedFlashing : GhostState
        {
            public FrightenedFlashing(Ghost ghost) : base(FRIGHTENEDFLASHING) { ghost.OnFrightenedFlashingState(); }
        }

        private class Eyes : GhostState
        {
            public Eyes(Ghost ghost) : base(EYES) { ghost.OnEyesState(); }
        }
    }
}
