using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework.Input;
using Pacman.Engine.Display;
using Microsoft.Xna.Framework;

namespace Pacman.Scenes
{
    class AnimationTest : SceneObject
    {
        List<DisplayObject> _allSprites;
        public AnimationTest()
            : base("AnimationTest")
        {
            _allSprites = new List<DisplayObject>();

            Translate(35, 35);

            for (var i = 0; i < 4; i++)
            {
                DisplayObject ghostGroup = new DisplayObject();

                ghostGroup.AddComponent(new AnimatedSpriteObject("pacman"));

                AnimatedSpriteObject eyes = new AnimatedSpriteObject("pacman");
                eyes.AddSequence("Eyes", 16, 5, 5000);
                ghostGroup.AddComponent(eyes);

                AnimatedSpriteObject pupils = new AnimatedSpriteObject("pacman");
                pupils.AddSequence("Pupils", 21, 5, 5000);
                pupils.Tint = new Color(60, 87, 167);
                ghostGroup.AddComponent(pupils);

                _allSprites.Add(ghostGroup);
            }

            var blinky = (_allSprites[0] as DisplayObject)[0] as AnimatedSpriteObject;
            var pinky = (_allSprites[1] as DisplayObject)[0] as AnimatedSpriteObject;
            var inky = (_allSprites[2] as DisplayObject)[0] as AnimatedSpriteObject;
            var clyde = (_allSprites[3] as DisplayObject)[0] as AnimatedSpriteObject;

            blinky.AddSequence("BlinkyUp", 8, 8, 250);
            blinky.Tint = Color.Red;

            pinky.AddSequence("PinkyUp", 8, 8, 250);
            pinky.Tint = Color.Pink;

            inky.AddSequence("InkyUp", 8, 8, 250);
            inky.Tint = Color.Cyan;

            clyde.AddSequence("ClydeUp", 8, 8, 250);
            clyde.Tint = Color.Orange;


            var frightenedGroup = new DisplayObject();
            var frightenedGhost = new AnimatedSpriteObject("pacman");
            var frightenedEyes = new SpriteObject("pacman", 28);
            frightenedGhost.AddSequence("Frightened", 8, 8, 250);
            frightenedGhost.Tint = new Color(60, 87, 167);
            frightenedEyes.Tint = new Color(255, 207, 50);
            frightenedGroup.AddComponent(frightenedGhost);
            frightenedGroup.AddComponent(frightenedEyes);

            var frightenedGroup1 = new DisplayObject();
            var frightenedGhost1 = new AnimatedSpriteObject("pacman");
            var frightenedEyes1 = new SpriteObject("pacman", 28);
            frightenedGhost1.AddSequence("Frightened", 8, 8, 250);
            frightenedGhost1.Tint = Color.White;
            frightenedEyes1.Tint = Color.Red;
            frightenedGroup1.AddComponent(frightenedGhost1);
            frightenedGroup1.AddComponent(frightenedEyes1);
            
            var pacman = new AnimatedSpriteObject("pacman");
            pacman.AddSequence("PacmanChomp", new[] { 0, 1, 2, 3, 4, 5, 6, 7, 6, 5, 4, 3, 2, 1 }, 250);
            pacman.Tint = Color.Yellow;

            var pellet = new AnimatedSpriteObject("pacman");
            pellet.AddSequence("Pellet", 26, 1, 10000);
            pellet.Tint = new Color(255, 225, 200);

            var powerPellet = new AnimatedSpriteObject("pacman");
            powerPellet.AddSequence("PowerPellet", 27, 1, 10000);

            _allSprites.Add(frightenedGroup);
            _allSprites.Add(frightenedGroup1);
            _allSprites.Add(pacman);
            _allSprites.Add(pellet);
            _allSprites.Add(powerPellet);

            var itemsPerRow = 16;
            var spacing = 70;
            for (var i = 0; i < _allSprites.Count; i++)
            {
                AddComponent(_allSprites[i]);
                _allSprites[i].Translate((i % itemsPerRow) * spacing, ((float)Math.Floor((decimal)i / itemsPerRow) * spacing));
                //_allSprites[i].Resize(0.5f);
            }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            if (InputHelper.IsPressed(Keys.T) || InputHelper.IsPressed(Keys.Escape))
            {
                Stage.GotoScene("Menu");
            }
            else if (InputHelper.IsPressed(Keys.Escape))
            {
                Stage.MainGame.Exit();
            }
        }
    }
}
