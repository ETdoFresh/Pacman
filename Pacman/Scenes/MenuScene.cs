using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine;
using Microsoft.Xna.Framework.Input;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework;
using Pacman.Engine.Display;

namespace Pacman.Scenes
{
    class MenuScene : SceneObject
    {
        public MenuScene()
            : base("Menu")
        {
            Translate(32, 32);

            var pacman = new ImageObject("pacman");
            pacman.Translate(256, 256);
            pacman.Resize(1.1f);
            AddComponent(pacman);

            var pacman2 = new SpriteObject("pacman", 8);
            pacman2.Translate(550, 0);
            AddComponent(pacman2);

            var pacman3 = new AnimatedSpriteObject("pacman");
            pacman3.AddSequence("Chomp", new[] { 0, 1, 2, 3, 4, 5, 6, 7, 6, 5, 4, 3, 2, 1 }, 500);
            pacman3.Translate(625, 0);
            AddComponent(pacman3);

            var rect = new RectangleObject(32, 32);
            rect.Translate(550, 50);
            AddComponent(rect);

            var circ = new CircleObject(16);
            circ.Translate(600, 50);
            AddComponent(circ);

            var text = new TextObject("Woot!\n1234567890\nREADY!\n1UP 2UP\nGAME   OVER");
            text.Translate(575, 150);
            text.Resize(1.5f);
            text.Rotate(-15);
            text.Tint = Color.Lime;
            AddComponent(text);

            var text2 = new TextObject("Press {Spacebar} to proceed to next Scene!\nPress {T} to view Animation Test!");
            text2.Translate(350, 350);
            text2.Resize(2);
            text2.Rotate(-5);
            AddComponent(text2);
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
            else if (InputHelper.IsPressed(Keys.Q) || InputHelper.IsPressed(Keys.Escape))
            {
                Stage.GameObject.Exit();
            }
        }
    }
}
