using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DisplayLibrary;

namespace Pacman
{
    abstract class GhostTarget : IDisposable
    {
        protected Ghost Ghost;
        protected Pacman Pacman;
        protected RectangleObject ImmediateTarget;
        protected RectangleObject Target;

        public GhostTarget(Ghost Ghost, Pacman Pacman, GroupObject displayParent)
        {
            this.Ghost = Ghost;
            this.Pacman = Pacman;

            var dimension = new Dimension(TileEngine.TileWidth, TileEngine.TileHeight);
            Target = new RectangleObject(displayParent, Pacman.Position, dimension);
        }

        public void Dispose()
        {
            Target.Dispose();
        }
    }

    class BlinkyTarget : GhostTarget
    {
        public BlinkyTarget(Ghost blinky, Pacman pacman, GroupObject displayParent = null)
            : base(blinky, pacman, displayParent)
        {
            Target.Color = Color.Red;
            Target.Alpha = 0.75f;
        }

        private void UpdateTarget(GameTime gameTime)
        {
        }
    }

    class PinkyTarget : GhostTarget
    {
        public PinkyTarget(Ghost pinky, Pacman pacman, GroupObject displayParent = null)
            : base(pinky, pacman, displayParent)
        {
            Target.Color = Color.Pink;
            Target.Alpha = 0.75f;
        }

        private void UpdateTarget(GameTime gameTime)
        {
        }
    }

    class InkyTarget : GhostTarget
    {
        public InkyTarget(Ghost inky, Pacman pacman, GroupObject displayParent = null)
            : base(inky, pacman, displayParent)
        {
            Target.Color = Color.Cyan;
            Target.Alpha = 0.75f;
        }

        private void UpdateTarget(GameTime gameTime)
        {
        }
    }

    class ClydeTarget : GhostTarget
    {
        public ClydeTarget(Ghost clyde, Pacman pacman, GroupObject displayParent = null)
            : base(clyde, pacman, displayParent)
        {
            Target.Color = Color.Orange;
            Target.Alpha = 0.75f;
        }

        private void UpdateTarget(GameTime gameTime)
        {
        }
    }
}
