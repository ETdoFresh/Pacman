using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PacmanGame
{
    abstract class GhostState
    {
        protected Ghost ghost;
        public virtual void Initialize(Ghost ghost) { this.ghost = ghost; }
        public virtual void UpdateTargetTile(Map map, Pacman pacman) { }
    }

    class BlinkyBase : GhostState
    {
        public override void Initialize(Ghost ghost)
        {
            ghost.SetFrame(0);
            ghost.debugRectangle.Color = Color.Red * 0.5f;
            base.Initialize(ghost);
        }
    }

    class BlinkyScatter : BlinkyBase
    {
        public override void UpdateTargetTile(Map map, Pacman pacman)
        {
            ghost.TargetTile = map.Tiles[map.MapWidth - 1, 0];
        }
    }

    class BlinkyChase : BlinkyBase
    {
        public override void UpdateTargetTile(Map map, Pacman pacman)
        {
            ghost.TargetTile = map.GetTileFromPosition(pacman.Position);
        }
    }

    class PinkyBase : GhostState
    {
        public override void Initialize(Ghost ghost)
        {
            ghost.SetFrame(8);
            ghost.debugRectangle.Color = Color.Pink * 0.5f;
            base.Initialize(ghost);
        }
    }

    class PinkyChase : PinkyBase
    {
        public override void UpdateTargetTile(Map map, Pacman pacman)
        {
            var pacmanTile = map.GetTileFromPosition(pacman.Position);
            ghost.TargetTile = map.GetTileFromDirectionClamped(pacmanTile, pacman.Direction * 4);
        }
    }

    class PinkyScatter : PinkyBase
    {
        public override void UpdateTargetTile(Map map, Pacman pacman)
        {
            ghost.TargetTile = map.Tiles[0, 0];
        }
    }

    class InkyBase : GhostState
    {
        public override void Initialize(Ghost ghost)
        {
            ghost.SetFrame(16);
            ghost.debugRectangle.Color = Color.Cyan * 0.5f;
            base.Initialize(ghost);
        }
    }

    class InkyChase : InkyBase
    {
        public static IStatic Blinky { get; set; }

        public override void UpdateTargetTile(Map map, Pacman pacman)
        {
            var pacmanTile = map.GetTileFromPosition(pacman.Position);
            ghost.TargetTile = map.GetTileFromDirectionClamped(pacmanTile, pacman.Direction * 2);
            if (Blinky != null && ghost.TargetTile != null)
            {
                var difference = Blinky.Position - ghost.TargetTile.Position;
                difference *= 2;
                var targetPosition = Blinky.Position + difference;
                targetPosition.X = MathHelper.Clamp(targetPosition.X, 0, map.TileWidth * map.MapWidth - 1);
                targetPosition.Y = MathHelper.Clamp(targetPosition.Y, 0, map.TileHeight * map.MapHeight - 1);
                ghost.TargetTile = map.GetTileFromPosition(targetPosition);
            }
        }
    }

    class InkyScatter : InkyBase
    {
        public override void UpdateTargetTile(Map map, Pacman pacman)
        {
            ghost.TargetTile = map.Tiles[map.MapWidth - 1, map.MapHeight - 1];
        }
    }

    class ClydeBase : GhostState
    {
        public override void Initialize(Ghost ghost)
        {
            ghost.SetFrame(24);
            ghost.debugRectangle.Color = Color.Orange * 0.5f;
            base.Initialize(ghost);
        }

    }

    class ClydeChase : ClydeBase
    {
        public override void UpdateTargetTile(Map map, Pacman pacman)
        {
            var pacmanTile = map.GetTileFromPosition(pacman.Position);
            ghost.TargetTile = pacmanTile;
            var distance = (ghost.Position - pacman.Position).Length();
            if (distance < 15 * 8)
                ghost.TargetTile = map.GetTileFromDirectionClamped(pacmanTile, new Vector2(-100, 100));
        }
    }

    class FrightenedState : GhostState
    {
        public Tile target;
        private Random random = new Random();

        public override void Initialize(Ghost ghost)
        {
            ghost.SetFrame(32);
            ghost.debugRectangle.Color = Color.Orange * 0.5f;
            ghost.Speed = 50;
            target = null;
            base.Initialize(ghost);
        }

        public override void UpdateTargetTile(Map map, Pacman pacman)
        {
            if (target == null)
            {
                target = map.Tiles[random.Next(0, map.MapWidth), random.Next(0, map.MapHeight)];
                ghost.TargetTile = target;
            }

            base.UpdateTargetTile(map, pacman);
        }
    }

    class EatenState : GhostState
    {
        public Tile target;
        public GhostState PreviousState { get; set; }

        public override void Initialize(Ghost ghost)
        {
            ghost.SetFrame(50);
            ghost.debugRectangle.Color = Color.Green * 0.5f;
            ghost.Speed = 400;

            base.Initialize(ghost);
        }

        public override void UpdateTargetTile(Map map, Pacman pacman)
        {
            if (target == null)
            {
                target = map.Tiles[13, 11];
                ghost.TargetTile = target;
            }

            if (PreviousState != null && ghost.Position == target.Position)
            {
                ghost.State = PreviousState;
            }

            base.UpdateTargetTile(map, pacman);
        }
    }

    class InkyHome : InkyBase
    {

    }
}
