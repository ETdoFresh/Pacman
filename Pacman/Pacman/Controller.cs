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
        Ghost ghost;

        public Controller()
        {
            map = new Map();
            pellets = Pellet.CreateAllPellets(map);

            pacman = new Pacman();
            pacman.Position = map.Tiles[13, 23].Position;
            pacman.DesiredDirection = new Vector2(-1, 0);

            ghost = new Ghost();
            ghost.Position = map.Tiles[13, 11].Position;

            player = new Player() { Score = 0 };

            gameObjects.Add(map);
            foreach (var pellet in pellets)
                gameObjects.Add(pellet);
            gameObjects.Add(pacman);
            gameObjects.Add(ghost);
            
        }

        public void Update(GameTime gameTime)
        {
            UpdatePlayerDesiredDirectionFromKeyboard();
            UpdatePlayerTargetFromDesiredDirection();
            CheckIfPlayerAtePellet();
            UpdateGhostDirectionFromAI();

            foreach (var gameObject in gameObjects)
                gameObject.Update(gameTime);

            WrapAroundScreen(pacman);
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

        private void UpdateGhostDirectionFromAI()
        {
            var pacmanTile = map.GetTileFromPosition(pacman.Position);
            var ghostTile = map.GetTileFromPosition(ghost.Position);
            if (ghost.NextTile == null && ghost.NextNextTile == null)
            {
                ghost.NextTile = map.GetTileFromDirection(ghostTile, new Vector2(-1, 0));
                ghost.NextNextTile = map.GetTileFromDirection(ghostTile, new Vector2(-2, 0));
            }
            else if (ghost.Position == ghost.NextTile.Position)
            {
                ghost.NextTile = ghost.NextNextTile;
                Tile[] checkTiles = new Tile[4] 
                {
                    map.GetTileFromDirection(ghost.NextTile, new Vector2(-1, 0)),
                    map.GetTileFromDirection(ghost.NextTile, new Vector2(1, 0)),
                    map.GetTileFromDirection(ghost.NextTile, new Vector2(0, -1)),
                    map.GetTileFromDirection(ghost.NextTile, new Vector2(0, 1))
                };

                Tile nextNextTile = null;
                float distance = 0;
                for (int i = 0; i < checkTiles.Length; i++)
                {
                    if (checkTiles[i] != null && checkTiles[i] != ghostTile && checkTiles[i].IsPassable)
                    {
                        if (nextNextTile != null)
                        {
                            var newDistance = (pacman.Position - checkTiles[i].Position).LengthSquared();
                            if (newDistance < distance)
                            {
                                distance = newDistance;
                                nextNextTile = checkTiles[i];
                            }
                        }
                        else
                        {
                            nextNextTile = checkTiles[i];
                            distance = (pacman.Position - nextNextTile.Position).LengthSquared();
                        }
                    }
                }
                ghost.NextNextTile = nextNextTile;
            }
            ghost.SetTarget(ghost.NextTile);
        }
    }
}
