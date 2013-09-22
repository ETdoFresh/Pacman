using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;

namespace Pacman.Objects
{
    class DotCounter : GameObject
    {
        public delegate void DotLimitReachedHandler(Ghost ghost);
        public event DotLimitReachedHandler DotLimitReached = delegate { };

        protected int _numberDotsEaten;
        int _dotLimit;
        Ghost _ghost;

        public DotCounter(int dotLimit, Ghost ghost)
        {
            _numberDotsEaten = 0;
            _dotLimit = dotLimit;
            _ghost = ghost;
            if (_numberDotsEaten >= _dotLimit)
                DotLimitReached(_ghost);
        }

        public virtual void AddDot()
        {
            _numberDotsEaten++;
            if (_numberDotsEaten >= _dotLimit)
                DotLimitReached(_ghost);
        }

        protected void CallDotLimitReached(Ghost ghost)
        {
            DotLimitReached(ghost);
        }

        public override void RemoveSelf()
        {
            base.RemoveSelf();
            DotLimitReached = null;
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
