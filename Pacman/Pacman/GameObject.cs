using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;

namespace Pacman
{
    abstract class GameObject : IDisposable
    {
        public Position Position { get; set; }
        public Rotation Rotation { get; set; }
        public Velocity Velocity { get; set; }
        public TilePosition TilePosition { get; set; }

        protected List<IDisposable> disposables = new List<IDisposable>();

        public virtual void Dispose()
        {
            foreach (var disposable in disposables)
                disposable.Dispose();
        }
    }
}
