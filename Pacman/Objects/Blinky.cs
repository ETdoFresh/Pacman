using Microsoft.Xna.Framework;
using Pacman.Engine.Helpers;
using Pacman.Engine.Display;

namespace Pacman.Objects
{
    class Blinky : Ghost
    {
        public Blinky(TileGrid tileGrid, PacmanObject pacmanObject)
            : base(tileGrid, pacmanObject) { }

        static public Blinky Create(TileGrid tileGrid, PacmanObject pacmanObject)
        {
            Blinky result = new Blinky(tileGrid, pacmanObject);
            result.Create();
            return result;
        }

        protected override void SetTransforms()
        {
            base.SetTransforms();
            Translate(_tileGrid.GetPosition(13.5f, 11f));
        }

        protected override void SetProperties()
        {
            base.SetProperties();
            Target = new Target.Blinky(_pacman);
            ImmediateTarget = new Target.Immediate(this, _tileGrid);
            _tileGrid.AddComponent(Target);
            _tileGrid.AddComponent(ImmediateTarget);
        }

        public override void ResetProperties()
        {
            base.ResetProperties();
            Body.Tint = Color.Red;
        }
    }
}
