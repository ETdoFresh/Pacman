using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Pacman.Engine.Helpers;

namespace Pacman.Objects
{
    class Pellet : DisplayObject
    {
        public Pellet() { }

        public override void RemoveSelf()
        {
            if (TilePosition != null) TilePosition.RemoveSelf();
            if (SpriteObject != null) SpriteObject.RemoveSelf();
            base.RemoveSelf();
        }

        public TilePosition TilePosition { get; set; }
        public SpriteObject SpriteObject { get; set; }
    }
}
