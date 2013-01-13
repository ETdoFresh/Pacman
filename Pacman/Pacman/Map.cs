using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pacman.DisplayObject;

namespace Pacman
{
    class Map : GroupObject
    {
        public Map() : base ()
        {
            display.Stage.Insert(this);
        }
    }
}
