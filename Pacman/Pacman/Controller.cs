using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

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
            player = new Player();
            player.Position = map.Tiles[13, 23].Position;
            player.DesiredDirection = new Vector2(-1, 0);

            gameObjects.Add(map);
            gameObjects.Add(player);
        }

        public void Update(GameTime gameTime)
        {
            SetDesiredDirectionFromKeyboard();
            UpdatePlayerTargetFromDesiredDirection();

            foreach (var gameObject in gameObjects)
                gameObject.Update(gameTime);

            WrapAroundScreen();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in gameObjects)
                gameObject.Draw(spriteBatch);
        }

        private void WrapAroundScreen()
        {
            var mapXLimit = map.MapWidth * map.TileWidth;
            var mapYLimit = map.MapHeight * map.TileHeight;

            if (player.Position.X < 0)
                player.Position = new Vector2(mapXLimit - 1, player.Position.Y);
            else if (player.Position.X >= mapXLimit)
                player.Position = new Vector2(0, player.Position.Y);

            if (player.Position.Y < 0)
                player.Position = new Vector2(player.Position.X, mapYLimit - 1);
            else if (player.Position.Y >= mapYLimit)
                player.Position = new Vector2(player.Position.X, 0);
        }

        private void SetDesiredDirectionFromKeyboard()
        {
            var keyboardState = Keyboard.GetState();
            var keyDictionary = new Dictionary<Keys, Vector2>
            {
                {Keys.Left, new Vector2(-1, 0)},
                {Keys.Right, new Vector2(1, 0)},
                {Keys.Up, new Vector2(0, -1)},
                {Keys.Down, new Vector2(0, 1)},
            };

            var newDirection = Vector2.Zero;
            foreach (var key in keyDictionary)
                if (keyboardState.IsKeyDown(key.Key))
                    newDirection += key.Value;

            if (newDirection != Vector2.Zero)
                player.DesiredDirection = newDirection;
        }

        private void UpdatePlayerTargetFromDesiredDirection()
        {
            var currentTile = map.GetTileFromPosition(player.Position);
            Tile nextTile = null;
            if (player.DesiredDirection.X != 0)
            {
                player.Direction = new Vector2(player.DesiredDirection.X, 0);
                nextTile = map.GetTileFromDirection(currentTile, player.Direction);
                
                // Create new temp tile if wrapping across the screen
                if (nextTile == null)
                    nextTile = new Tile(player.Position + player.Direction * 32);
            }
            if (nextTile == null || !nextTile.IsPassable && player.DesiredDirection.Y != 0)
            {
                player.Direction = new Vector2(0, player.DesiredDirection.Y);
                nextTile = map.GetTileFromDirection(currentTile, player.Direction);
            }
            if (nextTile == null || !nextTile.IsPassable)
            {
                player.Direction = player.PreviousDirection;
                nextTile = map.GetTileFromDirection(currentTile, player.Direction);
            }

            if (nextTile != null && nextTile.IsPassable)
                player.SetTarget(nextTile);
        }
    }
}
