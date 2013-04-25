﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DisplayLibrary;

namespace Pacman
{
    abstract class EndTarget : IDisposable
    {
        protected Position Position;
        protected Ghost Ghost;
        protected Pacman Pacman;
        protected RectangleObject Target;

        public TilePosition TilePosition { get; private set; }

        public EndTarget(Ghost Ghost, Pacman Pacman, GroupObject displayParent)
        {
            this.Ghost = Ghost;
            this.Pacman = Pacman;
            this.Position = new Position();
            TilePosition = new TilePosition(Position);

            var dimension = new Dimension(TileEngine.TileWidth, TileEngine.TileHeight);
            Target = new RectangleObject(displayParent, Position, dimension);
        }

        public virtual void Dispose()
        {
            TilePosition.Dispose();
            Target.Dispose();
        }
    }

    class BlinkyTarget : EndTarget
    {
        public BlinkyTarget(Ghost blinky, Pacman pacman, GroupObject displayParent = null)
            : base(blinky, pacman, displayParent)
        {
            Target.Color = Color.Red;
            Target.Alpha = 0.75f;
            Position.Value = TileEngine.GetPosition(27, 5).Value;

            if (blinky.State == GhostState.Chase)
                Runtime.GameUpdate += UpdateChaseTarget;
        }

        private void UpdateChaseTarget(GameTime gameTime)
        {
            Position.Value = Pacman.Position.Value;
        }

        public override void Dispose()
        {
            Runtime.GameUpdate -= UpdateChaseTarget;
            base.Dispose();
        }
    }

    class PinkyTarget : EndTarget
    {
        public PinkyTarget(Ghost pinky, Pacman pacman, GroupObject displayParent = null)
            : base(pinky, pacman, displayParent)
        {
            Target.Color = Color.Pink;
            Target.Alpha = 0.75f;
            Position.Value = TileEngine.GetPosition(0, 5).Value;

            if (pinky.State == GhostState.Chase)
                Runtime.GameUpdate += UpdateChaseTarget;
        }

        private void UpdateChaseTarget(GameTime gameTime)
        {
            Position.Value = Pacman.Position.Value + Pacman.Direction.GetVectorOffset() * 4 * TileEngine.TileWidth;
        }

        public override void Dispose()
        {
            Runtime.GameUpdate -= UpdateChaseTarget;
            base.Dispose();
        }
    }

    class InkyTarget : EndTarget
    {
        private Ghost blinky;

        public InkyTarget(Ghost inky, Ghost blinky, Pacman pacman, GroupObject displayParent = null)
            : base(inky, pacman, displayParent)
        {
            this.blinky = blinky;

            Target.Color = Color.Cyan;
            Target.Alpha = 0.75f;
            Position.Value = TileEngine.GetPosition(26, 29).Value;

            if (inky.State == GhostState.Chase)
                Runtime.GameUpdate += UpdateChaseTarget;
        }

        private void UpdateChaseTarget(GameTime gameTime)
        {
            var pacmanOffset = Pacman.Position.Value + Pacman.Direction.GetVectorOffset() * 2 * TileEngine.TileWidth;
            var blinkyOffset = pacmanOffset - blinky.Position.Value;
            Position.Value = blinky.Position.Value + blinkyOffset * 2;
        }

        public override void Dispose()
        {
            Runtime.GameUpdate -= UpdateChaseTarget;
            base.Dispose();
        }
    }

    class ClydeTarget : EndTarget
    {
        public ClydeTarget(Ghost clyde, Pacman pacman, GroupObject displayParent = null)
            : base(clyde, pacman, displayParent)
        {
            Target.Color = Color.Orange;
            Target.Alpha = 0.75f;
            Position.Value = TileEngine.GetPosition(1, 29).Value;

            if (clyde.State == GhostState.Chase)
                Runtime.GameUpdate += UpdateChaseTarget;
        }

        private void UpdateChaseTarget(GameTime gameTime)
        {
            if (Vector2.Distance(Ghost.Position.Value, Pacman.Position.Value) > 8 * TileEngine.TileWidth)
                Position.Value = Pacman.Position.Value;
            else
                Position.Value = TileEngine.GetPosition(0, 30).Value;
        }

        public override void Dispose()
        {
            Runtime.GameUpdate -= UpdateChaseTarget;
            base.Dispose();
        }
    }
}
