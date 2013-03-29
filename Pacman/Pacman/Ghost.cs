using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;

namespace Pacman
{
    abstract class Ghost : GameObject
    {
        public AnimatedSprite AnimatedSprite { get; set; }
        public Target KeyboardTarget { get; set; }
        public Steering SeekKeyboardTarget { get; set; }

        public Ghost(GroupObject displayParent = null)
        {
            DisplayParent = displayParent;
            Position = new Position();
            Rotation = new Rotation();
            TilePosition = new TilePosition(Position);
        }

        public Ghost(Ghost old)
        {
            DisplayParent = old.DisplayParent;
            Position = old.Position;
            Rotation = old.Rotation;
            TilePosition = old.TilePosition;
            old.Dispose();
        }
    }

    class Blinky : Ghost
    {
        public Blinky(GroupObject displayParent = null) : base(displayParent) { Initialize(); }
        public Blinky(Ghost oldState) : base(oldState) { Initialize(); }

        private void Initialize()
        {
            AnimatedSprite = new AnimatedSprite(filename: "pacman", position: Position, parent: DisplayParent);
            AnimatedSprite.AddSequence(name: "move", frames: new int[] { 0, 1 }, time: 150);
            AnimatedSprite.SetSequence(name: "move");
            Velocity = new Velocity(Position);
            //Collision = new Collision(Position);
            disposables = new List<IDisposable>() { AnimatedSprite, Velocity };
        }
    }

    class Pinky : Ghost
    {
        public Pinky(GroupObject displayParent = null) : base(displayParent) { Initialize(); }
        public Pinky(Ghost oldState) : base(oldState) { Initialize(); }

        private void Initialize()
        {
            AnimatedSprite = new AnimatedSprite(filename: "pacman", position: Position, parent: DisplayParent);
            AnimatedSprite.AddSequence(name: "move", start: 8, count: 2, time: 150);
            AnimatedSprite.SetSequence(name: "move");
            Velocity = new Velocity(Position);
            //Collision = new Collision(Position);
            disposables = new List<IDisposable>() { AnimatedSprite, Velocity };
        }
    }

    class Inky : Ghost
    {
        public Inky(GroupObject displayParent = null) : base(displayParent) { Initialize(); }
        public Inky(Ghost oldState) : base(oldState) { Initialize(); }

        private void Initialize()
        {
            AnimatedSprite = new AnimatedSprite(filename: "pacman", position: Position, parent: DisplayParent);
            AnimatedSprite.AddSequence(name: "move", start: 16, count: 2, time: 150);
            AnimatedSprite.SetSequence(name: "move");
            Velocity = new Velocity(Position);
            //Collision = new Collision(Position);
            disposables = new List<IDisposable>() { AnimatedSprite, Velocity };
        }
    }

    class Clyde : Ghost
    {
        public Clyde(GroupObject displayParent = null) : base(displayParent) { Initialize(); }
        public Clyde(Ghost oldState) : base(oldState) { Initialize(); }

        private void Initialize()
        {
            AnimatedSprite = new AnimatedSprite(filename: "pacman", position: Position, parent: DisplayParent);
            AnimatedSprite.AddSequence(name: "move", start: 24, count: 2, time: 150);
            AnimatedSprite.SetSequence(name: "move");
            Velocity = new Velocity(Position);
            //Collision = new Collision(Position);
            disposables = new List<IDisposable>() { AnimatedSprite, Velocity };
        }
    }

    class Frightened : Ghost
    {
        public Frightened(GroupObject displayParent = null) : base(displayParent) { Initialize(); }
        public Frightened(Ghost oldState) : base(oldState) { Initialize(); }

        private void Initialize()
        {
            AnimatedSprite = new AnimatedSprite(filename: "pacman", position: Position, parent: DisplayParent);
            AnimatedSprite.AddSequence(name: "move", start: 32, count: 2, time: 150);
            AnimatedSprite.SetSequence(name: "move");
            Velocity = new Velocity(Position);
            //Collision = new Collision(Position);
            disposables = new List<IDisposable>() { AnimatedSprite, Velocity };
        }
    }
}
