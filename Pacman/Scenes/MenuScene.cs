using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine;
using Microsoft.Xna.Framework.Input;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework;

namespace Pacman.Scenes
{
    class MenuScene : SceneObject
    {
        public MenuScene() : base("Menu")
        {
            Translate(20, 20);

            var pacman = new ImageObject("pacman");
            pacman.Translate(125, 125);
            pacman.Resize(1.1f);
            AddChild(pacman);

            var pacman2 = new SpriteObject("pacman", 5);
            pacman2.Translate(300, 0);
            AddChild(pacman2);

            var pacman3 = new AnimatedSpriteObject("pacman");
            pacman3.AddSequence("Chomp", new[] { 36, 37, 36, 38, }, 150);
            pacman3.Translate(350, 0);
            AddChild(pacman3);

            var rect = new RectangleObject(32, 32);
            rect.Translate(300, 50);
            AddChild(rect);

            var circ = new CircleObject(16);
            circ.Translate(350,50);
            AddChild(circ);

            var text = new TextObject("Woot!");
            text.Translate(300, 100);
            text.Resize(1.5f);
            text.Rotate(-15);
            text.Tint = Color.Lime;
            AddChild(text);

            var text2 = new TextObject("Press Spacebar to proceed to next Scene!");
            text2.Translate(325, 350);
            text2.Resize(2);
            text2.Rotate(-5);
            AddChild(text2);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if (InputHelper.IsPressed(Keys.Space))
            {
                Stage.GotoScene("Level");
            }
            else if (InputHelper.IsPressed(Keys.T))
            {
                Stage.GotoScene("AnimationTest");
            }
            else if (InputHelper.IsPressed(Keys.Escape))
            {
                Stage.Game.Exit();
            }
        }
    }
}
