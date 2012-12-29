using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        Map map;
        PlayerManager playerManager;
        MapManager mapManager;
        private TileMap tileMap;
        private Texture2D rect;

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
            var textureRectangles = TexturePacker.GetTextureRectangles(Content.RootDirectory + "\\" + filename + ".xml");

            tileMap = new TileMap(texture, textureRectangles);

            rect = new Texture2D(graphics.GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.White });
            Camera.Position = new Vector2(50, 50);

            /*
            player = new Player(texture, textureRectangles);
            blinky = new Blinky(texture, textureRectangles);
            pinky = new Pinky(texture, textureRectangles);
            inky = new Inky(texture, textureRectangles);
            clyde = new Clyde(texture, textureRectangles);
            map = new Map(texture, textureRectangles);

            var ghosts = new List<Ghost>() { blinky, pinky, inky, clyde };

            mapManager = new MapManager(map, player, ghosts);
            playerManager = new PlayerManager(player);
             */
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            /*
            player.Update(gameTime);
            blinky.Update(gameTime);
            pinky.Update(gameTime);
            inky.Update(gameTime);
            clyde.Update(gameTime);

            mapManager.Update(gameTime);
            playerManager.Update(gameTime);
            */

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            /*
            player.Draw(spriteBatch);
            blinky.Draw(spriteBatch);
            pinky.Draw(spriteBatch);
            inky.Draw(spriteBatch);
            clyde.Draw(spriteBatch);
            map.Draw(spriteBatch);
            */
            tileMap.Draw(spriteBatch);

            var mouseState = Mouse.GetState();
            var screenCoordinates = new Vector2(mouseState.X, mouseState.Y);
            var worldCoordinates = Camera.GetWorldCoordinatesFromScreenCoordinates(screenCoordinates);
            var tilePosition = tileMap.GetTilePositionFromWorldCoordinates(worldCoordinates);

            if (tilePosition.X > -1 && tilePosition.Y > -1)
            {
                var screenTilePosition = Camera.GetScreenCoordinatesFromWorldCoordinates(tilePosition);
                var highlightRectangle = new Rectangle((int)screenTilePosition.X - tileMap.TileWidth / 2, (int)screenTilePosition.Y - tileMap.TileHeight / 2, tileMap.TileWidth, tileMap.TileHeight);
                spriteBatch.Draw(rect, highlightRectangle, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
