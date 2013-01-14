using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pacman.DisplayEngine;
using System;
using System.Collections.Generic;

namespace Pacman
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Controller controller;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 500;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            display.Content = Content;
            display.graphics = graphics;
            display.font = Content.Load<SpriteFont>("SpriteFont");

            var map = new Map();
            var player = new Player(map);
            var ghost = new Ghost(map);

            controller = new Controller(map, player, ghost);

            map.X = display.ContentWidth / 2 - map.Width / 2;
            map.Y = display.ContentHeight / 2 - map.Height / 2;

            player.X = map.Tiles[14, 17].X;
            player.Y = map.Tiles[14, 17].Y + map.TileWidth / 2;

            ghost.X = map.Tiles[14, 11].X;
            ghost.Y = map.Tiles[14, 11].Y + map.TileWidth / 2;
            ghost.Target = player;

            DebugManager.Player = player;
            DebugManager.Ghost = ghost;
            DebugManager.Map = map;
            DebugManager.Rectangles = new List<RectangleObject>()
            {
                display.NewRect(map, 0, 0, map.TileWidth, map.TileHeight),
                display.NewRect(map, 0, 0, map.TileWidth, map.TileHeight)
            };
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            display.Stage.Update(gameTime);
            controller.Update(gameTime);
            DebugManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            display.Stage.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
