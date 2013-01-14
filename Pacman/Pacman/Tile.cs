using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.DisplayEngine;

namespace Pacman
{
    class Tile : GroupObject
    {
        public static float Width { get; set; }
        public static float Height { get; set; }

        public bool IsPassable { get; set; }
        public int XTile { get { return (int)(X / Tile.Width); } }
        public int YTile { get { return (int)(Y / Tile.Height); } }

        public Tile(GroupObject parent = null) : base() 
        {
            if (parent == null)
                parent = display.Stage;
            parent.Insert(this);

            IsPassable = true;
        }
    }
}
