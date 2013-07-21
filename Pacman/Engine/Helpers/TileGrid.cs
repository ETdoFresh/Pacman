using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman.Engine.Helpers
{
    public enum OriginType { TopLeft, Center }

    class TileGrid
    {
        int _numberOfXTiles;
        int _numberOfYTiles;
        int _tileWidth;
        int _tileHeight;
        OriginType _originType;
        TileGridData[,] _grid;

        public TileGridData[,] Data { get { return _grid; } }
        public int NumberOfXTiles { get { return _numberOfXTiles; } }
        public int NumberOfYTiles { get { return _numberOfYTiles; } }
        public int TileWidth { get { return _tileWidth; } }
        public int TileHeight { get { return _tileHeight; } }
        public int Width { get { return _numberOfXTiles * _tileWidth; } }
        public int Height { get { return _numberOfYTiles * _tileHeight; } }

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
                _grid = new TileGridData[_numberOfXTiles, _numberOfYTiles];

            // Create an origin based on OriginType
            var origin = Vector2.Zero;
            if (_originType == OriginType.Center)
                origin = new Vector2(_tileWidth / 2, _tileHeight / 2);

            // Populate grid with new TileGridData
            for (var x = 0; x < _grid.GetLength(0); x++)
            {
                for (var y = 0; y < _grid.GetLength(1); y++)
                {
                    var tileGridData = new TileGridData();
                    tileGridData.RectangleBounds = new Rectangle(_tileWidth * x, _tileHeight * y, _tileWidth, _tileHeight);
                    tileGridData.PositionVector = new Vector2(_tileWidth * x + origin.X, _tileHeight * y + origin.Y);
                    _grid[x, y] = tileGridData;
                }
            }
        }

        public class TileGridData
        {
            public Rectangle RectangleBounds { get; set; }
            public Vector2 PositionVector { get; set; }
        }
    }
}
