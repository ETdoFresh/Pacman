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

        protected override void SetAnimations()
        {
            base.SetAnimations();
            Body.Tint = Color.Red;
        }

        protected override void SetTransforms()
        {
            Translate(_tileGrid.GetPosition(13.5f, 11f));
            base.SetTransforms();
        }

        public override void OnChaseState()
        {
            base.OnChaseState();
            Target.ChangeState(Target.BLINKY, _pacman);
        }

        public override void OnScatterState()
        {
            base.OnScatterState();
            Target.Translate(_tileGrid.GetPosition(25, -3));
        }
    }
}
