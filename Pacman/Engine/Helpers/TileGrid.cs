using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pacman.Engine.Display;

namespace Pacman.Engine.Helpers
{
    public enum OriginType { TopLeft, Center }

    class TileGrid : GroupObject
    {
        int _numberOfXTiles;
        int _numberOfYTiles;
        int _tileWidth;
        int _tileHeight;
        OriginType _originType;
        Tile[,] _grid;

        public Tile[,] Data { get { return _grid; } }
        public int NumberOfXTiles { get { return _numberOfXTiles; } }
        public int NumberOfYTiles { get { return _numberOfYTiles; } }
        public int TileWidth { get { return _tileWidth; } }
        public int TileHeight { get { return _tileHeight; } }
        public override float Width { get { return _numberOfXTiles * _tileWidth; } }
        public override float Height { get { return _numberOfYTiles * _tileHeight; } }

        public TileGrid(int numberOfXTiles, int numberOfYTiles, int tileWidth, int tileHeight)
        {
            _numberOfXTiles = numberOfXTiles;
            _numberOfYTiles = numberOfYTiles;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _originType = OriginType.Center;

            UpdateGrid();
        }

        private void UpdateGrid()
        {
            // Create a new grid if not created or different size
            if (_grid == null || _grid.GetLength(0) != _numberOfXTiles || _grid.GetLength(1) != _numberOfYTiles)
                _grid = new Tile[_numberOfXTiles, _numberOfYTiles];

            // Create an origin based on OriginType
            var origin = Vector2.Zero;
            if (_originType == OriginType.Center)
                origin = new Vector2(_tileWidth / 2, _tileHeight / 2);

            // Populate grid with new TileGridData
            for (var x = 0; x < _grid.GetLength(0); x++)
            {
                for (var y = 0; y < _grid.GetLength(1); y++)
                {
                    var tile = new Tile(_tileWidth, _tileHeight);
                    tile.Translate(x * _tileWidth + _tileWidth / 2, y * _tileHeight + _tileHeight / 2);
                    AddChild(tile);
                    _grid[x, y] = tile;
                }
            }
        }
    }
}
