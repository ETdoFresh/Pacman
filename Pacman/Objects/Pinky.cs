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

        protected override void SetTransforms()
        {
            _startPosition = _tileGrid.GetPosition(13.5f, 14);
            Translate(_startPosition);
            base.SetTransforms();
        }

        protected override void ResetProperties()
        {
            base.ResetProperties();
            Body.Tint = Color.Pink;
        }

        public override void OnChaseState()
        {
            base.OnChaseState();
            Target.ChangeState(Target.PINKY, _pacman);
        }

        public override void OnScatterState()
        {
            base.OnScatterState();
            Target.Translate(_tileGrid.GetPosition(2, -3));
        }
    }
}
