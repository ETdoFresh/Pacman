using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using DisplayEngine;

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
        protected DebugInformation debugInformation;

        public Controller()
        {
            map = new Map();
            pellets = Pellet.CreateAllPellets(map);
            

            pacman = new Pacman();
            pacman.AddEventListener("test", print);
            pacman.PositionTest = map.Tiles[13, 23].Position;
            pacman.DesiredDirection = new Vector2(-1, 0);

            var blinky = new Ghost(new BlinkyChase());
            var pinky = new Ghost(new PinkyChase());
            var inky = new Ghost(new InkyChase());
            var clyde = new Ghost(new ClydeChase());
            var eyes = new Ghost(new EatenState());
            var frightened = new Ghost(new FrightenedState());
            
            blinky.Position = map.Tiles[13, 11].Position;
            pinky.Position = map.Tiles[13, 13].Position;
            inky.Position = map.Tiles[11, 13].Position;
            clyde.Position = map.Tiles[15, 13].Position;
            eyes.Position = map.Tiles[13, 23].Position;
            frightened.Position = map.Tiles[3, 3].Position;

            InkyChase.Blinky = blinky;

            ghosts = new List<Ghost>() { blinky, pinky, inky, clyde, eyes, frightened };

            player = new Player() { Score = 0 };

            gameObjects.Add(map);
            foreach (var pellet in pellets)
                gameObjects.Add(pellet);
            gameObjects.Add(pacman);
            foreach (var ghost in ghosts)
                gameObjects.Add(ghost);

            foreach (var pellet in pellets)
            {
                pellet.AddEventListener("PelletEaten", this.OnPelletEaten);
                pellet.AddEventListener("PelletEaten", player.OnPelletEaten);
            }

            debugInformation = new DebugInformation(this);
        }

        

        private void print(object sender, EventArgs e)
        {
            System.Console.WriteLine(sender + " " + ((PropertyChangeEventArgs)e).NewValue + " " + ((PropertyChangeEventArgs)e).OldValue);
        }

        public void Update(GameTime gameTime)
        {
            UpdatePlayerDesiredDirectionFromKeyboard();
            UpdatePlayerTargetFromDesiredDirection();
            CheckPlayerCollisions();
            foreach (var ghost in ghosts)
                ghost.UpdateGhostTiles(map, pacman);

            WrapAroundScreen(pacman);

            foreach (var ghost in ghosts)
                WrapAroundScreen(ghost);

            if (pacman.X > map.MapWidth * map.TileWidth)
                throw new Exception();

            debugInformation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
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
                pacman.Target = nextTile;
        }

        private void CheckPlayerCollisions()
        {
            var currentPlayerTile = map.GetTileFromPosition(pacman.Position);
            for (int i = 0; i < pellets.Count(); i++)
            {
                var pelletTile = map.GetTileFromPosition(pellets[i].Position);
                if (currentPlayerTile == pelletTile)
                    pellets[i].DispatchEvent("PelletEaten", new EventArgs());
            }
            for (int i = 0; i < ghosts.Count(); i++)
            {
                var ghostTile = map.GetTileFromPosition(ghosts[i].Position);
                if (currentPlayerTile == ghostTile)
                    ghosts[i].DispatchEvent("GhostCollidePacman", new EventArgs());
            }
        }

        private void OnPelletEaten(object sender, EventArgs e)
        {
            Pellet pellet = (Pellet)sender;
            gameObjects.Remove(pellet);
            pellets.Remove(pellet);
            System.Console.WriteLine("Pellet Eaten! " + player.Score);
        }

        private void OnGhostCollidePacman(object sender, EventArgs e)
        {
            Ghost ghost = (Ghost)sender;
        }

        public class DebugInformation
        {
            Controller controller;
            TextObject textObject;

            public DebugInformation(Controller controller)
            {
                this.controller = controller;
                textObject = display.NewText("Test");
            }

            public void Update(GameTime gameTime)
            {
                var updatedString = string.Format("Score: {0}", controller.player.Score);
                textObject.Text = updatedString;
                textObject.X = textObject.Width / 2;
                textObject.Y = textObject.Height / 2;
            }
        }
    }
}
