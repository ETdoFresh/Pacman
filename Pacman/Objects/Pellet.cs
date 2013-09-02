using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Pacman.Engine.Helpers;

namespace Pacman.Objects
{
    class Pellet : GroupObject
    {
        public Pellet() { }

        public TilePosition TilePosition { get; set; }
        public SpriteObject SpriteObject { get; set; }
    }
}
