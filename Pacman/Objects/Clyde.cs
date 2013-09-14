using Microsoft.Xna.Framework;
using Pacman.Engine.Helpers;
using Pacman.Engine.Display;

namespace Pacman.Objects
{
    class Clyde : Ghost
    {
        public Clyde(TileGrid tileGrid, PacmanObject pacmanObject)
            : base(tileGrid, pacmanObject) { }

        static public Clyde Create(TileGrid tileGrid, PacmanObject pacmanObject)
        {
            Clyde result = new Clyde(tileGrid, pacmanObject);
            result.Create();
            return result;
        }

        protected override void SetAnimations()
        {
            base.SetAnimations();
            Body.Tint = Color.Orange;
        }

        protected override void SetTransforms()
        {
            Translate(_tileGrid.GetPosition(15.5f, 14f));
            base.SetTransforms();
        }

        public override void OnChaseState()
        {
            base.OnChaseState();
            Target.ChangeState(Target.CLYDE, _pacman, this);
        }

        public override void OnScatterState()
        {
            base.OnScatterState();
            Target.Translate(_tileGrid.GetPosition(0, 31));
        }
    }
}
