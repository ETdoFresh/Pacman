using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class Controller : IGameObject
    {
        List<IGameObject> gameObjects = new List<IGameObject>();
        Player player;
        Map map;

        public Controller()
        {
            map = new Map();
            player = new Player(new Vector2(600, 100));
            player.IsSteering = true;

            gameObjects.Add(map);
            gameObjects.Add(player);
        }

        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var tileX = (float)Math.Floor((decimal)mouseState.X / map.TileWidth) * map.TileWidth;
            var tileY = (float)Math.Floor((decimal)mouseState.Y / map.TileHeight) * map.TileHeight;
            player.SetTarget(tileX, tileY);

            foreach (var gameObject in gameObjects)
                gameObject.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in gameObjects)
                gameObject.Draw(spriteBatch);
        }
    }
}
