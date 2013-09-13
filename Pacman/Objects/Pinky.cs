using Microsoft.Xna.Framework;
using Pacman.Engine.Helpers;
using Pacman.Engine.Display;

namespace Pacman.Objects
{
    class Pinky : Ghost
    {
        public Pinky(TileGrid tileGrid, PacmanObject pacmanObject)
            : base(tileGrid, pacmanObject) { }

        static public Pinky Create(TileGrid tileGrid, PacmanObject pacmanObject)
        {
            Pinky result = new Pinky(tileGrid, pacmanObject);
            result.Create();
            return result;
        }

        protected override void SetAnimations()
        {
            base.SetAnimations();
            Body.Tint = Color.Pink;
        }


        protected override void SetTransforms()
        {
            base.SetTransforms();
            Translate(_tileGrid.GetPosition(11.5f, 14f));
        }

        protected override void SetProperties()
        {
            base.SetProperties();
            Target = new Target();
            ImmediateTarget = new Target();
            ImmediateTarget.ChangeState(Target.IMMEDIATE, _tileGrid, this);
            _tileGrid.AddComponent(Target);
            _tileGrid.AddComponent(ImmediateTarget);
        }

        public override void OnChaseState()
        {
            base.OnChaseState();
            Target.ChangeState(Target.PINKY, _pacman);
        }
    }
}
