using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine;
using Microsoft.Xna.Framework;
using Pacman.Engine.Helpers;
using Pacman.Engine.Display;

namespace Pacman.Objects
{
    class Pacman : GroupObject, ISteer
    {
        public AnimatedSpriteObject AnimatedSprite { get; set; }
        public Speed Speed { get; set; }
        public Velocity Velocity { get; set; }
        public Steering Steering { get; set; }
        public TilePosition TilePosition { get; set; }
        public SnapToTarget SnapToTarget { get; set; }
        public Wrap Wrap { get; set; }

        public Pacman()
        {
            AnimatedSprite = new AnimatedSpriteObject("pacman");
            AnimatedSprite.AddSequence("Chomp", new[] { 36, 37, 36, 38 }, 150);
            AnimatedSprite.AddSequence("Die", 39, 11, 1000);
            AnimatedSprite.SetSequence("Chomp");
            AddChild(AnimatedSprite);

            Speed = new Speed(100);
            Velocity = new Velocity(Position);
            AddChild(Velocity);
        }

        public void ActivateTilePosition(int tileWidth, int tileHeight)
        {
            TilePosition = new TilePosition(Position, tileWidth, tileHeight);
            AddChild(TilePosition);
        }

        public void SteerTowards(DisplayObject target)
        {
            if (Steering != null)
            {
                RemoveChild(Steering);
                Steering.UnloadContent();
            }

            Steering = new Steering(this, target);
            AddChild(Steering);

            SnapToTarget = new SnapToTarget(this, Velocity, target);
            AddChild(SnapToTarget);
        }

        public void WrapAround(int left, int top, float right, float bottom)
        {
            Wrap = new Wrap(Position, left, top, right, bottom);
            AddChild(Wrap);
        }
    }
}
