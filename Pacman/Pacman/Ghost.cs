using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pacman.DisplayEngine;

namespace Pacman
{
    class Ghost: SteeringObject
    {
        public bool IsDead { get; set; }

        public Ghost(GroupObject parent = null)
            : base(display.RetrieveTexture("pacman"), display.RetrieveSourceRectangles("pacman"))
        {
            if (parent == null)
                parent = display.Stage;
            parent.Insert(this);

            Speed = 200;
            addSequence("Still", 4, 1, 0);
            setSequence("Still");

            var animationTime = 150;
            addSequence("Up", 0, 2, animationTime);
            addSequence("Down", 2, 2, animationTime);
            addSequence("Left", 4, 2, animationTime);
            addSequence("Right", 6, 2, animationTime);
            Play();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!IsDead)
                UpdateSequence();
        }

        private void UpdateSequence()
        {
            Play();
            if (Velocity.X < 0)
                setSequence("Left");
            else if (Velocity.X > 0)
                setSequence("Right");
            else if (Velocity.Y < 0)
                setSequence("Up");
            else if (Velocity.Y > 0)
                setSequence("Down");
            else
                Pause();
        }
    }
}
