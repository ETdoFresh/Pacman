using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine;
using Microsoft.Xna.Framework.Input;
using Pacman.Engine.Helpers;

namespace Pacman.Scenes
{
    class MenuScene : SceneObject
    {
        InputHelper _inputHelper;

        public MenuScene() : base("Menu")
        {
            var pacman = new ImageObject("pacman");
            AddChild(pacman);

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
                Stage.GotoScene("Level");
            }
            else if (_inputHelper.IsPressed(Keys.Escape))
                Stage.Back();
        }
    }
}
