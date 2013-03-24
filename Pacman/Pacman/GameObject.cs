using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;

namespace Pacman
{
    abstract class GameObject : IDisposable
    {
        protected List<IDisposable> disposables = new List<IDisposable>();

        public virtual void Dispose()
        {
            foreach (var disposable in disposables)
                disposable.Dispose();

            disposables = new List<IDisposable>();
        }

        public Position Position { get; set; }
        public Rotation Rotation { get; set; }
        public Velocity Velocity { get; set; }

    }
}
