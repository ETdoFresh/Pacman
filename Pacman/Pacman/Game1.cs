using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Xml.Linq;

namespace Pacman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Blinky blinky;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 450;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var filename = "pacman";
            var texture = Content.Load<Texture2D>(filename);
            var textureRectangles = LoadTextureRectangles(Content.RootDirectory + "\\" + filename + ".xml");

            player = new Player(texture, textureRectangles);
            player.Position = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            blinky = new Blinky(texture, textureRectangles);
            blinky.Position = new Vector2(player.Position.X + player.Width + 10, player.Position.Y);
        }

        private List<Rectangle> LoadTextureRectangles(string filename)
        {
            var textureRectangles = new List<Rectangle>();
            var doc = XElement.Load(filename);
            var query = from ele in doc.Elements("sprite") select ele;
            foreach (var ele in query)
            {
                var name = ele.Attribute("n").Value;
                var x = int.Parse(ele.Attribute("x").Value);
                var y = int.Parse(ele.Attribute("y").Value);
                var width = int.Parse(ele.Attribute("w").Value);
                var height = int.Parse(ele.Attribute("h").Value);
                textureRectangles.Add(new Rectangle(x, y, width, height));
            }
            return textureRectangles;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            player.Update(gameTime);
            blinky.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            player.Draw(spriteBatch);
            blinky.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
