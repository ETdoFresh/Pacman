using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;

namespace Pacman
{
    class Collision : IDisposable
    {
        private Position position;

        public Collision(Position position)
        {
            this.position = position;
        }

        public void Dispose()
        {
        }
    }
}
