﻿using Microsoft.Xna.Framework;
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
            Translate(_tileGrid.GetPosition(11.5f, 14f));
            base.SetTransforms();
        }

        public override void OnChaseState()
        {
            base.OnChaseState();
            Target.ChangeState(Target.INKY, _pacman, _blinky);
        }

        public override void OnScatterState()
        {
            base.OnScatterState();
            Target.Translate(_tileGrid.GetPosition(27, 31));
        }
    }
}
