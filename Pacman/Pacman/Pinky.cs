using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class Pinky : Ghost
    {
        public Pinky(Texture2D texture, List<Rectangle> textureRectangles)
            : base(texture, textureRectangles)
        {
            this.sequences = new List<Sequence>()
            {
                new Sequence(name: "Still", start: 8, count: 1, time: 0),
                new Sequence(name: "MoveUp", start: 8, count: 2, time: 200),
                new Sequence(name: "MoveDown", start: 10, count: 2, time: 200),
                new Sequence(name: "MoveLeft", start: 12, count: 2, time: 200),
                new Sequence(name: "MoveRight", start: 14, count: 2, time: 200),
                new Sequence(name: "Eatable", start: 32, count: 2, time: 200),
                new Sequence(name: "EatableFlashing", frames: new List<int>{32,33,32,33,34,35,34,35}, time: 400),
                new Sequence(name: "EatableFastFlashing", start: 32, count: 4, time: 200),
            };

            setSequence("Still");
        }
    }
}
