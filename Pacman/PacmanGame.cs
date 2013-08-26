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
using Pacman.Engine;
using Pacman.Scenes;
using Pacman.Engine.Display;

namespace Pacman
{
    public class PacmanGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        Stage _stage;

        public PacmanGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _stage = new Stage(this);
            _stage.GotoScene(new MenuScene());
            _stage.LoadScene(new AnimationTest());
            _stage.LoadScene(new LevelScene());
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameObject.SpriteBatch.Begin();
            base.Draw(gameTime);
            GameObject.SpriteBatch.End();
        }
    }
}
