using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Objects
{
    class GhostState
    {
        public enum GhostStates { Home, LeavingHome, Chase, Scatter, Frightened, Eyes }
        static public GhostStates HOME { get { return GhostStates.Home; } }
        static public GhostStates LEAVINGHOME { get { return GhostStates.LeavingHome; } }
        static public GhostStates CHASE { get { return GhostStates.Chase; } }
        static public GhostStates SCATTER { get { return GhostStates.Scatter; } }
        static public GhostStates FRIGHTENED { get { return GhostStates.Frightened; } }
        static public GhostStates EYES { get { return GhostStates.Eyes; } }

        protected GhostStates _ghostState;

        private GhostState(GhostStates ghostState)
        {
            _ghostState = ghostState;
        }

        static public GhostState Create(GhostStates ghostState)
        {
            switch (ghostState)
            {
                case GhostStates.Home:
                    return new Home();
                case GhostStates.LeavingHome:
                    return new LeavingHome();
                case GhostStates.Chase:
                    return new Chase();
                case GhostStates.Scatter:
                    return new Scatter();
                case GhostStates.Frightened:
                    return new Frightened();
                case GhostStates.Eyes:
                    return new Eyes();
                default:
                    throw new Exception("Ghost State not valid");
            }
        }

        private class Home : GhostState
        {
            public Home() : base(HOME) { }
        }

        private class LeavingHome : GhostState
        {
            public LeavingHome() : base(LEAVINGHOME) { }
        }

        private class Chase : GhostState
        {
            public Chase() : base(CHASE) { }
        }

        private class Scatter : GhostState
        {
            public Scatter() : base(SCATTER) { }
        }

        private class Frightened : GhostState
        {
            public Frightened() : base(FRIGHTENED) { }
        }

        private class Eyes : GhostState
        {
            public Eyes() : base(EYES) { }
        }
    }
}
