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
    class Pacman : GameObject
    {
        public enum State { Normal, Dead, Invisible }

        public AnimatedSprite AnimatedSprite { get; set; }
        public Target KeyboardTarget { get; set; }
        public Steering SeekKeyboardTarget { get; set; }
        public Collision Collision { get; set; }

        public Pacman()
        {
            Position = new Position(x: 150, y: 150);
            Rotation = new Rotation();
            setState(State.Normal);
        }

        public void setState(State state)
        {
            ResetState();
            if (state == State.Normal)
            {
                AnimatedSprite = new AnimatedSprite(filename: "pacman", position: Position, rotation: Rotation);
                AnimatedSprite.AddSequence(name: "chomp", frames: new int[] { 36, 37, 36, 38 }, time: 200);
                AnimatedSprite.AddSequence(name: "still", frames: new int[] { 36 });
                AnimatedSprite.SetSequence("chomp");
                Velocity = new Velocity(Position);
                KeyboardTarget = new Target(source: this);
                SeekKeyboardTarget = new Steering(this, KeyboardTarget);
                SeekKeyboardTarget.MaxSpeed = 150;
                Collision = new Collision(Position);
                disposables = new List<IDisposable>() { AnimatedSprite, Velocity, KeyboardTarget, SeekKeyboardTarget, Collision };
            }
            else if (state == State.Dead)
            {
                AnimatedSprite = new AnimatedSprite(filename: "pacman", position: Position);
                AnimatedSprite.AddSequence(name: "die", start: 39, count: 11, time: 1000);
                AnimatedSprite.SetSequence("die");
                AnimatedSprite.CurrentFrame = 0;
                //animatedSprite.EndSequence += OnEndOfDeathSequence;
                disposables = new List<IDisposable>() { AnimatedSprite };
            }
            else if (state == State.Invisible)
            {
            }
        }

        private void OnEndOfDeathSequence(object sender, EventArgs eventArgs)
        {
            setState(State.Normal);
        }

        private void ResetState()
        {
            var position = Position;
            Dispose();
            Position = position;
        }
    }
}
