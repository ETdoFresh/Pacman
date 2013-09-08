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
            base.SetTransforms();
            Translate(_tileGrid.GetPosition(15.5f, 14f));
        }

        protected override void SetProperties()
        {
            base.SetProperties();
            Target = new Target.Clyde(_pacman, this);
            ImmediateTarget = new Target.Immediate(this, _tileGrid);
            _tileGrid.AddComponent(Target);
            _tileGrid.AddComponent(ImmediateTarget);
        }
    }
}
