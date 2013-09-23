using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;

namespace Pacman.Objects
{
    class DotCounter : GameObject
    {
        public delegate void GhostDotLimitReachedHandler(Ghost ghost);
        public delegate void DotLimitReachedHandler();
        public event GhostDotLimitReachedHandler GhostDotLimitReached = delegate { };
        public event DotLimitReachedHandler DotLimitReached = delegate { };

        protected int _numberDotsEaten;
        int _dotLimit;
        Ghost _ghost;

        public DotCounter(int dotLimit, Ghost ghost)
        {
            _numberDotsEaten = 0;
            _dotLimit = dotLimit;
            _ghost = ghost;
            if (IsDotLimitReached())
                GhostDotLimitReached(_ghost);
        }

        public virtual void AddDot()
        {
            _numberDotsEaten++;
            if (IsDotLimitReached())
            {
                GhostDotLimitReached(_ghost);
                DotLimitReached();
            }
        }

        protected void CallDotLimitReached(Ghost ghost)
        {
            GhostDotLimitReached(ghost);
        }

        public override void RemoveSelf()
        {
            base.RemoveSelf();
            GhostDotLimitReached = null;
            DotLimitReached = null;
        }

        public bool IsDotLimitReached()
        {
            return _numberDotsEaten >= _dotLimit;
        }

        public void SetNewLimit(int dotLimit)
        {
            _dotLimit = dotLimit;
        }
    }

    class GlobalDotCounter : DotCounter
    {
        int _limit1, _limit2, _limit3;
        Ghost _ghost1, _ghost2, _ghost3;

        public GlobalDotCounter(int limit1, int limit2, int limit3, Ghost ghost1, Ghost ghost2, Ghost ghost3)
            : base(1, null)
        {
            _limit1 = limit1;
            _limit2 = limit2;
            _limit3 = limit3;
            _ghost1 = ghost1;
            _ghost2 = ghost2;
            _ghost3 = ghost3;
        }

        public override void AddDot()
        {
            _numberDotsEaten++;
            if (_numberDotsEaten == _limit1)
                CallDotLimitReached(_ghost1);
            if (_numberDotsEaten == _limit2)
                CallDotLimitReached(_ghost2);
            if (_numberDotsEaten == _limit3)
                CallDotLimitReached(_ghost3);
        }
    }
}
