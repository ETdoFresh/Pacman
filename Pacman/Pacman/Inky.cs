using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class Inky : Ghost
    {
        public Inky(Texture2D texture, List<Rectangle> textureRectangles)
            : base(texture, textureRectangles)
        {
            this.sequences = new List<Sequence>()
            {
                new Sequence(name: "Still", start: 16, count: 1, time: 0),
                new Sequence(name: "MoveUp", start: 16, count: 2, time: 200),
                new Sequence(name: "MoveDown", start: 18, count: 2, time: 200),
                new Sequence(name: "MoveLeft", start: 20, count: 2, time: 200),
                new Sequence(name: "MoveRight", start: 22, count: 2, time: 200),
                new Sequence(name: "Eatable", start: 32, count: 2, time: 200),
                new Sequence(name: "EatableFlashing", frames: new List<int>{32,33,32,33,34,35,34,35}, time: 400),
                new Sequence(name: "EatableFastFlashing", start: 32, count: 4, time: 200),
            };

            setSequence("Still");
        }
    }
}
