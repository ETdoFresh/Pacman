using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework.Input;

namespace Pacman.Scenes
{
    class LevelScene : SceneObject
    {
        private InputHelper _inputHelper;
        public LevelScene() : base("Level")
        {
            var pacman2 = new SpriteObject("pacman", 5);
            pacman2.Translate(300, 0);
            AddChild(pacman2);

            var pacman3 = new AnimatedSpriteObject("pacman");
            pacman3.AddSequence("Chomp", new[] { 36, 37, 36, 38, }, 150);
            pacman3.Translate(350, 0);
            AddChild(pacman3);

            _inputHelper = new InputHelper();
            AddChild(_inputHelper);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if (_inputHelper.IsPressed(Keys.Space))
            {
                Stage.SuspendScene(this);
                Stage.GotoScene("Menu");
            }
            else if (_inputHelper.IsPressed(Keys.Escape))
                Stage.Back();
        }
    }
}
