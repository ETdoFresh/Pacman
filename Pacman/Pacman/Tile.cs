using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.DisplayEngine;

namespace Pacman
{
    class Tile : GroupObject
    {
        public bool IsPassable { get; set; }

        public Tile(GroupObject parent = null) : base() 
        {
            if (parent == null)
                parent = display.Stage;
            parent.Insert(this);

            IsPassable = true;
        }
    }
}
