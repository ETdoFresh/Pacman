using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class DotCounter : IDisposable
    {
        private int value;
        public int Value { get { return value; } set { this.value = value; CheckLimit(); } }
        public int Limit { get; set; }

        public delegate void LimitReachedHandler();
        public event LimitReachedHandler LimitReached = delegate { };

        public DotCounter(int limit = 0)
        {
            Limit = limit;
            Value = 0;
        }

        private void CheckLimit()
        {
            if (Value == Limit)
                LimitReached();
        }

        public void Dispose()
        {
            LimitReached = null;
        }
    }
}
