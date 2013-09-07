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

            Stage.Create(this);
            Stage.LoadScene(new AnimationTest());
            Stage.LoadScene(new LevelScene());
            Stage.GotoScene(new MenuScene());
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            DisplayObject.SpriteBatch.Begin();
            base.Draw(gameTime);
            DisplayObject.SpriteBatch.End();
        }
    }
}
