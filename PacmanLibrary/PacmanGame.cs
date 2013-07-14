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
using PacmanLibrary.Engine;

namespace PacmanLibrary
{
    public class PacmanGame
    {
        Game _game;
        GraphicsDeviceManager _graphics;
        ContentManager _content;
        SpriteBatch _spriteBatch;
        Image _pacman;
        RenderContext _renderContext;
        Sprite _pacman2;
        AnimatedSprite _pacman3, _pacman4, _pacman5;

        public PacmanGame(Game game, GraphicsDeviceManager graphics, ContentManager content)
        {
            _game = game;
            _graphics = graphics;
            _content = content;
            _renderContext = new RenderContext();
            _renderContext.GraphicsDevice = graphics.GraphicsDevice;
        }

        public void Initialize()
        {
            _pacman = new Image("pacman");
            _pacman.Position = new Vector2(300, 20);
         
            _pacman2 = new Sprite("pacman", 12);

            _pacman3 = new AnimatedSprite("pacman");
            _pacman3.Position = new Vector2(40, 0);

            _pacman4 = new AnimatedSprite("pacman");
            _pacman4.Position = new Vector2(0, 40);
            _pacman4.AddSequence("RedGhost", new[] { 0, 1 }, 200);

            _pacman5 = new AnimatedSprite("pacman");
            _pacman5.Position = new Vector2(40, 40);
            _pacman5.AddSequence("PinkGhost", new[] { 8, 9 }, 200, 10);
        }

        public void LoadContent(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _renderContext.SpriteBatch = spriteBatch;

            _pacman.LoadContent(_content);
            _pacman2.LoadContent(_content);
            _pacman3.LoadContent(_content);
            _pacman4.LoadContent(_content);
            _pacman5.LoadContent(_content);
        }

        public void UnloadContent()
        {
            _pacman.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            _renderContext.GameTime = gameTime;

            _pacman.Update(_renderContext);
            _pacman2.Update(_renderContext);
            _pacman3.Update(_renderContext);
            _pacman4.Update(_renderContext);
            _pacman5.Update(_renderContext);
        }

        public void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _pacman.Draw(_renderContext);
            _pacman2.Draw(_renderContext);
            _pacman3.Draw(_renderContext);
            _pacman4.Draw(_renderContext);
            _pacman5.Draw(_renderContext);
            _spriteBatch.End();
        }
    }
}
