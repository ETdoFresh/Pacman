﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Blinky : Ghost
    {
        public Blinky(Texture2D texture, List<Rectangle> textureRectangles)
            : base(texture, textureRectangles)
        {
            this.sequences = new List<AnimationSequence>()
            {
                new AnimationSequence(name: "Still", start: 0, count: 1, time: 0),
                new AnimationSequence(name: "MoveUp", start: 0, count: 2, time: 200),
                new AnimationSequence(name: "MoveDown", start: 2, count: 2, time: 200),
                new AnimationSequence(name: "MoveLeft", start: 4, count: 2, time: 200),
                new AnimationSequence(name: "MoveRight", start: 6, count: 2, time: 200),
                new AnimationSequence(name: "Eatable", start: 32, count: 2, time: 200),
                new AnimationSequence(name: "EatableFlashing", frames: new List<int>{32,33,32,33,34,35,34,35}, time: 400),
                new AnimationSequence(name: "EatableFastFlashing", start: 32, count: 4, time: 200),
            };

            setSequence("Still");
        }
    }
}
