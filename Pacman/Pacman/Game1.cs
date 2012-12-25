using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Ghost blinky;
        Ghost pinky;
        Ghost inky;
        Ghost clyde;
        List<Tile> mapTiles = new List<Tile>();
        PlayerManager playerManager;
        CollisionManager collisionManager;

        Dictionary<Image, Texture2D> bbox = new Dictionary<Image, Texture2D>();

        public Game1()
        {
            this.IsMouseVisible = true;
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 500;
            Content.RootDirectory = "Content";
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
            player.X = 600;
            playerManager = new PlayerManager(player);

            blinky = new Blinky(texture, textureRectangles);
            pinky = new Pinky(texture, textureRectangles);
            inky = new Inky(texture, textureRectangles);
            clyde = new Clyde(texture, textureRectangles);

            blinky.X = graphics.GraphicsDevice.Viewport.Width / 2 + 40;
            blinky.Y = graphics.GraphicsDevice.Viewport.Height / 2;
            pinky.X = blinky.X + 40;
            pinky.Y = blinky.Y;
            inky.X = blinky.X + 80;
            inky.Y = blinky.Y;
            clyde.X = blinky.X + 120;
            clyde.Y = blinky.Y;
            clyde.Velocity = new Vector2(1, 0);

            var ghosts = new List<Ghost>(){ blinky, pinky, inky, clyde };

            for (int row = 0; row < Map.data.GetLength(0); row++)
                for (int column = 0; column < Map.data.GetLength(1); column++)
                {
                    if (Map.data[row, column] > 0)
                    {
                        var tileIndex = Map.data[row, column] + 53;
                        var newTile = new Tile(texture, textureRectangles, tileIndex);
                        var xPos = newTile.Width * column + newTile.Width / 2;
                        var yPos = newTile.Height * row + newTile.Height / 2;
                        newTile.Position = new Vector2(xPos, yPos);
                        newTile.Orientation = Map.rotation[row, column] * MathHelper.ToRadians(90);
                        mapTiles.Add(newTile);
                    }
                }

            for (int row = 0; row < Map.innerWallsData.GetLength(0); row++)
                for (int column = 0; column < Map.innerWallsData.GetLength(1); column++)
                {
                    if (Map.innerWallsData[row, column] > 0)
                    {
                        var tileIndex = Map.innerWallsData[row, column] + 53;
                        var newTile = new Tile(texture, textureRectangles, tileIndex);
                        var xPos = newTile.Width * column + newTile.Width / 2;
                        var yPos = newTile.Height * row + newTile.Height / 2;
                        newTile.Position = new Vector2(xPos, yPos);
                        newTile.Orientation = Map.innerWallsRotation[row, column] * MathHelper.ToRadians(90);
                        mapTiles.Add(newTile);
                    }
                }

            collisionManager = new CollisionManager(player, ghosts, mapTiles);
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
            pinky.Update(gameTime);
            inky.Update(gameTime);
            clyde.Update(gameTime);

            playerManager.Update(gameTime);
            collisionManager.Update(gameTime);

            foreach (var tile in mapTiles)
                tile.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            player.Draw(spriteBatch);
            blinky.Draw(spriteBatch);
            pinky.Draw(spriteBatch);
            inky.Draw(spriteBatch);
            clyde.Draw(spriteBatch);

            foreach (var tile in mapTiles)
                tile.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
