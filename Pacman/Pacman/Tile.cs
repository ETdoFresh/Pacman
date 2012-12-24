using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class Tile : Sprite
    {
        public Tile(Texture2D texture, List<Rectangle> textureRectangles, int tileIndex)
            : base (texture, textureRectangles)
        {
            this.sequences = new List<AnimationSequence>()
            {
                new AnimationSequence(name: "MapTile", start: tileIndex, count: 1, time: 0),
            };

            setSequence("MapTile");
        }
    }
}
