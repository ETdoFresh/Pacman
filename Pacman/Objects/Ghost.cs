using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class Ghost: GroupObject, ISteer
    {
        protected AnimatedSpriteObject _body;
        protected AnimatedSpriteObject _eyes;
        protected AnimatedSpriteObject _pupils;

        public Ghost()
        {
            _body = new AnimatedSpriteObject("pacman");
            _body.AddSequence("BodyFloat", 8, 8, 250);
            AddChild(_body);

            _eyes = new AnimatedSpriteObject("pacman");
            _eyes.AddSequence("Eyes", 16, 5, 5000);
            AddChild(_eyes);

            _pupils = new AnimatedSpriteObject("pacman");
            _pupils.AddSequence("Pupils", 21, 5, 5000);
            _pupils.Tint = new Color(60, 87, 167);
            AddChild(_pupils);

            Speed = new Speed(225);
            Velocity = new Velocity(Position);
            Direction = new Direction();
        }

        public Speed Speed { get; set; }
        public Velocity Velocity { get; set; }
        public Direction Direction { get; set; }
    }
}
