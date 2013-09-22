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

        protected override void SetTransforms()
        {
            _startPosition = _tileGrid.GetPosition(15.5f, 14);
            Translate(_startPosition);
            base.SetTransforms();
        }

        protected override void ResetProperties()
        {
            base.ResetProperties();
            Body.Tint = Color.Orange;
        }

        public override void OnLeavingHomeState()
        {
            base.OnLeavingHomeState();
            Direction.Value = Direction.LEFT;
            ShiftEyesToDirection.SetEyesByDirection();
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
