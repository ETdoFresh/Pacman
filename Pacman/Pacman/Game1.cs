using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pacman.DisplayObject;
using System;

namespace Pacman
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

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
            var rectangle = display.NewRect(10, 10, 20, 20);
            rectangle.Color = Color.BlueViolet;

            player.X = display.ContentWidth / 2;
            player.Y = display.ContentHeight / 2;

            ghost.X = player.X + 30;
            ghost.Y = player.Y;

            DebugManager.Player = player;
            DebugManager.Ghost = ghost;
            DebugManager.Map = map;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            display.Stage.Update(gameTime);
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
