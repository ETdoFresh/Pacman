using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;

namespace Pacman
{
    class Ghost : GameObject
    {
        public AnimatedSprite AnimatedSprite { get; set; }
        public Target KeyboardTarget { get; set; }
        public Steering SeekKeyboardTarget { get; set; }

        public Ghost()
        {
            Position = new Position(x: 50, y: 50);
            Rotation = new Rotation();
            Velocity = new Velocity(Position);

            AnimatedSprite = new AnimatedSprite(filename: "pacman", position: Position);
            AnimatedSprite.AddSequence(name: "move", frames: new int[] { 0, 1 }, time: 150);
            AnimatedSprite.SetSequence(name: "move");

            KeyboardTarget = new Target(source: this);
            SeekKeyboardTarget = new Steering(this, KeyboardTarget);

            //Collision = new Collision(Position);

            disposables = new List<IDisposable>() { AnimatedSprite, Velocity, KeyboardTarget, SeekKeyboardTarget };
        }
    }
}
