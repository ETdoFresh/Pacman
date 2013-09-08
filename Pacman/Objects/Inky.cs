using Microsoft.Xna.Framework;
using Pacman.Engine.Helpers;
using Pacman.Engine.Display;

namespace Pacman.Objects
{
    class Inky : Ghost
    {
        Ghost _blinky;

        public Inky(TileGrid tileGrid, PacmanObject pacmanObject, Ghost blinky)
            : base(tileGrid, pacmanObject) { _blinky = blinky; }

        static public Inky Create(TileGrid tileGrid, PacmanObject pacmanObject, Ghost blinky)
        {
            Inky result = new Inky(tileGrid, pacmanObject, blinky);
            result.Create();
            return result;
        }

        protected override void SetAnimations()
        {
            base.SetAnimations();
            Body.Tint = Color.Cyan;
        }

        protected override void SetTransforms()
        {
            base.SetTransforms();
            Translate(_tileGrid.GetPosition(13.5f, 14f));
        }

        protected override void SetProperties()
        {
            base.SetProperties();
            Target = new Target.Inky(_pacman, _blinky);
            ImmediateTarget = new Target.Immediate(this, _tileGrid);
            _tileGrid.AddComponent(Target);
            _tileGrid.AddComponent(ImmediateTarget);
        }
    }
}
