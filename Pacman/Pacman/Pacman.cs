using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using DisplayLibrary;

namespace Pacman
{
    abstract class Pacman : GameObject
    {
        public AnimatedSprite AnimatedSprite { get; set; }
        public PacmanTarget Target { get; set; }
        public Steering Steering { get; set; }
        public SnapToTarget SnapToTarget { get; set; }

        public Pacman(GroupObject displayParent = null)
        {
            DisplayParent = displayParent;
            Position = new Position();
            Rotation = new Rotation();
            TilePosition = new TilePosition(Position);
            Target = new PacmanTarget();
            Collision.AddGameObject(this);
        }

        public Pacman(Pacman old)
        {
            DisplayParent = old.DisplayParent;
            Position = old.Position;
            Rotation = old.Rotation;
            TilePosition = old.TilePosition;
            old.Dispose();
        }

        public override void Dispose()
        {
            Collision.RemoveGameObject(this);
            base.Dispose();
        }
    }

    class PacmanNormal : Pacman
    {
        public PacmanNormal(GroupObject displayParent = null) : base(displayParent) { Initialize(); }
        public PacmanNormal(Pacman oldState) : base(oldState) { Initialize(); }

        private void Initialize()
        {
            AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: DisplayParent, position: Position, rotation: Rotation);
            AnimatedSprite.AddSequence(name: "chomp", frames: new int[] { 36, 37, 36, 38 }, time: 200);
            AnimatedSprite.AddSequence(name: "still", frames: new int[] { 36 });
            AnimatedSprite.SetSequence("chomp");
            Velocity = new Velocity(Position);
            Steering = new Steering(this, Target);
            SnapToTarget = new SnapToTarget(this, Target, maxSpeed: 100);
            disposables = new List<IDisposable>() { AnimatedSprite, Steering, Velocity, SnapToTarget };
        }
    }

    class PacmanDead : Pacman
    {
        public PacmanDead(GroupObject displayParent = null) : base(displayParent) { Initialize(); }
        public PacmanDead(Pacman oldState) : base(oldState) { Initialize(); }

        private void Initialize()
        {
            AnimatedSprite = new AnimatedSprite(filename: "pacman", parent: DisplayParent, position: Position);
            AnimatedSprite.AddSequence(name: "die", start: 39, count: 11, time: 1000);
            AnimatedSprite.SetSequence("die");
            AnimatedSprite.CurrentFrame = 0;
            disposables = new List<IDisposable>() { AnimatedSprite };

            AnimatedSprite.EndSequence += OnEndOfDeathSequence;
        }

        private void OnEndOfDeathSequence()
        {
            Dispose();
        }

        public override void Dispose()
        {
            AnimatedSprite.EndSequence -= OnEndOfDeathSequence;
            base.Dispose();
        }
    }
}
