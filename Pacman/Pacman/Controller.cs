using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace PacmanGame
{
    class Controller : IGameObject
    {
        List<IGameObject> gameObjects = new List<IGameObject>();
        Player player;
        Pacman pacman;
        Map map;
        List<Pellet> pellets;
        List<Ghost> ghosts;

        public Controller()
        {
            map = new Map();
            pellets = Pellet.CreateAllPellets(map);

            pacman = new Pacman();
            pacman.Position = map.Tiles[13, 23].Position;
            pacman.DesiredDirection = new Vector2(-1, 0);

            
            var blinky = new Blinky();
            var pinky = new Pinky();
            var inky = new Inky();
            var clyde = new Clyde();
            blinky.Position = map.Tiles[13, 11].Position;
            pinky.Position = map.Tiles[13, 13].Position;
            inky.Position = map.Tiles[11, 13].Position;
            inky.Blinky = blinky;
            clyde.Position = map.Tiles[15, 13].Position;

            ghosts = new List<Ghost>() { blinky, pinky, inky, clyde };

            player = new Player() { Score = 0 };

            gameObjects.Add(map);
            foreach (var pellet in pellets)
                gameObjects.Add(pellet);
            gameObjects.Add(pacman);
            foreach (var ghost in ghosts)
                gameObjects.Add(ghost);
            
        }

        public void Update(GameTime gameTime)
        {
            UpdatePlayerDesiredDirectionFromKeyboard();
            UpdatePlayerTargetFromDesiredDirection();
            CheckIfPlayerAtePellet();
            foreach (var ghost in ghosts)
                ghost.UpdateGhostTiles(map, pacman);

            foreach (var gameObject in gameObjects)
                gameObject.Update(gameTime);

            WrapAroundScreen(pacman);

            foreach (var ghost in ghosts)
                WrapAroundScreen(ghost);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in gameObjects)
                gameObject.Draw(spriteBatch);
        }

        private void WrapAroundScreen(IStatic gameObject)
        {
            var mapXLimit = map.MapWidth * map.TileWidth;
            var mapYLimit = map.MapHeight * map.TileHeight;

            if (gameObject.Position.X < 0)
                gameObject.Position = new Vector2(mapXLimit - 1, gameObject.Position.Y);
            else if (gameObject.Position.X >= mapXLimit)
                gameObject.Position = new Vector2(0, gameObject.Position.Y);

            if (gameObject.Position.Y < 0)
                gameObject.Position = new Vector2(gameObject.Position.X, mapYLimit - 1);
            else if (gameObject.Position.Y >= mapYLimit)
                gameObject.Position = new Vector2(gameObject.Position.X, 0);
        }

        private void UpdatePlayerDesiredDirectionFromKeyboard()
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
                pacman.DesiredDirection = newDirection;
        }

        private void UpdatePlayerTargetFromDesiredDirection()
        {
            var currentTile = map.GetTileFromPosition(pacman.Position);
            Tile nextTile = null;
            if (pacman.DesiredDirection.X != 0)
            {
                pacman.Direction = new Vector2(pacman.DesiredDirection.X, 0);
                nextTile = map.GetTileFromDirection(currentTile, pacman.Direction);

                // Create new temp tile if wrapping across the screen
                if (nextTile == null)
                    nextTile = new Tile(pacman.Position + pacman.Direction * 32);
            }
            if (nextTile == null || !nextTile.IsPassable && pacman.DesiredDirection.Y != 0)
            {
                pacman.Direction = new Vector2(0, pacman.DesiredDirection.Y);
                nextTile = map.GetTileFromDirection(currentTile, pacman.Direction);
            }
            if (nextTile == null || !nextTile.IsPassable)
            {
                pacman.Direction = pacman.PreviousDirection;
                nextTile = map.GetTileFromDirection(currentTile, pacman.Direction);
            }

            if (nextTile != null && nextTile.IsPassable)
                pacman.SetTarget(nextTile);
        }

        private void CheckIfPlayerAtePellet()
        {
            var currentPlayerTile = map.GetTileFromPosition(pacman.Position);
            for (int i = 0; i < pellets.Count(); i++)
            {
                var pelletTile = map.GetTileFromPosition(pellets[i].Position);
                if (currentPlayerTile == pelletTile)
                {
                    gameObjects.Remove(pellets[i]);
                    player.Score += pellets[i].Score;
                    pellets.RemoveAt(i);
                }
            }
        }
    }
}
