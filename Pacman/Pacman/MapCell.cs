using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class MapCell
    {
        public int TileID {
            get { return tiles.Count > 0 ? tiles[0] : 0; }
            set 
            {
                if (tiles.Count > 0) tiles[0] = value;
                else tiles.Add(value);
            }
        }
        public float Orientation
        {
            get { return orientations.Count > 0 ? orientations[0] : 0; }
            set
            {
                if (orientations.Count > 0) orientations[0] = value;
                else orientations.Add(value);
            }
        }
        public List<int> tiles = new List<int>();
        public List<float> orientations = new List<float>();

        public MapCell(int tileID)
        {
            TileID = tileID;
        }

        public void Add(int tileId, float orientation)
        {
            tiles.Add(tileId);
            orientations.Add(orientation);
        }
    }
}
