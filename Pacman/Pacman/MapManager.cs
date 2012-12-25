using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class MapManager
    {
        private CollisionManager collisionManager;
        private MapTile[,] mapTiles;
        private Map map;

        public MapManager(Map map, Player player, List<Ghost> ghosts)
        {
            this.map = map;
            collisionManager = new CollisionManager(player, ghosts, this);

            mapTiles = new MapTile[map.MapWidth, map.MapHeight];
            for (int x = 0; x < map.MapWidth; x++)
            {
                for (int y = 0; y < map.MapHeight; y++)
                {
                    mapTiles[x, y] = new MapTile();
                    mapTiles[x, y].Position = new Vector2(x * map.TileWidth + map.TileWidth / 2, y * map.TileHeight + map.TileHeight / 2);
                    mapTiles[x, y].Width = map.TileWidth;
                    mapTiles[x, y].Height = map.TileHeight;
                    if (map.InnerWall[y, x] == null)
                        mapTiles[x, y].IsPassable = true;
                }
            }

            player.Position = mapTiles[13, 23].Position;
            player.X += map.TileWidth / 2;

            ghosts[0].Position = mapTiles[13, 11].Position;
            ghosts[0].X += map.TileWidth / 2;
            ghosts[2].Position = mapTiles[11, 14].Position;
            ghosts[2].X += map.TileWidth / 2;
            ghosts[1].Position = mapTiles[13, 14].Position;
            ghosts[1].X += map.TileWidth / 2;
            ghosts[3].Position = mapTiles[15, 14].Position;
            ghosts[3].X += map.TileWidth / 2;
        }

        public void Update(GameTime gameTime)
        {
            collisionManager.Update(gameTime);
        }

        public MapTile getMapTileFromPosition(Vector2 position)
        {
            var x = (int)Math.Floor(position.X / map.TileWidth);
            var y = (int)Math.Floor(position.Y / map.TileHeight);
            return mapTiles[x, y];
        }
    }

    struct MapTile
    {
        private Vector2 position;
        public Vector2 Position { get { return position; } set { position = value; } }
        public float X { get { return position.X; } }
        public float Y { get { return position.Y; } }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsPassable { get; set; }
        public Rectangle BoundingBox { get { return new Rectangle((int)X, (int)Y, Width, Height); } }
    }
}
