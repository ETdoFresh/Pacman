using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class AIState
    {
        public enum State { Home, LeavingHome, Chase, Scatter, Wander, Eyes }
        public const State HOME = State.Home;
        public const State LEAVINGHOME = State.LeavingHome;
        public const State CHASE = State.Chase;
        public const State SCATTER = State.Scatter;
        public const State WANDER = State.Wander;
        public const State EYES = State.Eyes;

        State _currentState;

        private AIState(State currentState)
        {
            _currentState = currentState; 
        }

        static public AIState Change(State ghostState, Ghost ghost)
        {
            switch (ghostState)
            {
                case HOME:
                    return new Home(ghost);
                case LEAVINGHOME:
                    return new LeavingHome(ghost);
                case CHASE:
                    return new Chase(ghost);
                case SCATTER:
                    return new Scatter(ghost);
                case WANDER:
                    return new Wander(ghost);
                case EYES:
                    return new Eyes(ghost);
                default:
                    throw new Exception("Ghost State not valid");
            }
        }

        public State CurrentState { get { return _currentState; } }

        private class Home : AIState
        {
            public Home(Ghost ghost) : base(HOME) { ghost.OnHomeState(); }
        }

        private class LeavingHome : AIState
        {
            public LeavingHome(Ghost ghost) : base(LEAVINGHOME) { ghost.OnLeavingHomeState(); }
        }

        private class Chase : AIState
        {
            public Chase(Ghost ghost) : base(CHASE) { ghost.OnChaseState(); }
        }

        private class Scatter : AIState
        {
            public Scatter(Ghost ghost) : base(SCATTER) { ghost.OnScatterState(); }
        }

        private class Wander : AIState
        {
            public Wander(Ghost ghost) : base(WANDER) { ghost.OnWanderState(); }
        }

        private class Eyes : AIState
        {
            public Eyes(Ghost ghost) : base(EYES) { ghost.OnEyesState(); }
        }
    }
}
