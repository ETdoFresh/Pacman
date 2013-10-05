using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.Objects
{
    class GhostState
    {
        public enum State { Home, Normal, Frightened, FlashingFrightened, Eyes }
        public const State HOME = State.Home;
        public const State NORMAL = State.Normal;
        public const State FRIGHTENED = State.Frightened;
        public const State FLASHINGFRIGHTENED = State.FlashingFrightened;
        public const State EYES = State.Eyes;

        private State _currentState;

        private GhostState(State state)
        {
            _currentState = state;
        }

        static public GhostState Create(State state, Ghost ghost)
        {
            switch (state)
            {
                case HOME:
                    return new Home(ghost);
                case NORMAL:
                    return new Normal(ghost);
                case FRIGHTENED:
                    return new Frightened(ghost);
                case FLASHINGFRIGHTENED:
                    return new FlashingFrightened(ghost);
                case EYES:
                    return new Eyes(ghost);
                default:
                    throw new Exception("State not valid, how did you get here?");
            }
        }

        public State CurrentState { get { return _currentState; } }

        private class Home : GhostState
        {
            public Home(Ghost ghost) : base(HOME) { ghost.OnHomeGhostState(); }
        }

        private class Normal : GhostState
        {
            public Normal(Ghost ghost) : base(NORMAL) { ghost.OnNormalGhostState(); }
        }

        private class Frightened : GhostState
        {
            public Frightened(Ghost ghost) : base(FRIGHTENED) { ghost.OnFrightenedGhostState(); }
        }

        private class FlashingFrightened : GhostState
        {
            public FlashingFrightened(Ghost ghost) : base(FLASHINGFRIGHTENED) { ghost.OnFlashingFrightenedGhostState(); }
        }

        private class Eyes : GhostState
        {
            public Eyes(Ghost ghost) : base(EYES) { ghost.OnEyesGhostState(); }
        }
    }
}
