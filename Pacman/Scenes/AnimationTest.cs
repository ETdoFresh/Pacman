using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework.Input;
using Pacman.Engine.Display;

namespace Pacman.Scenes
{
    class AnimationTest : SceneObject
    {
        List<AnimatedSpriteObject> _allSprites;
        public AnimationTest()
            : base("AnimationTest")
        {
            _allSprites = new List<AnimatedSpriteObject>();

            Translate(30, 30);

            for (var i = 0; i < 32; i++)
            {
                var sprite = new AnimatedSpriteObject("pacman");
                _allSprites.Add(sprite);
                AddChild(sprite);
            }

            _allSprites[0].AddSequence("BlinkyUp", 0, 2, 150);
            _allSprites[1].AddSequence("BlinkyDown", 2, 2, 150);
            _allSprites[2].AddSequence("BlinkyLeft", 4, 2, 150);
            _allSprites[3].AddSequence("BlinkyRight", 6, 2, 150);

            _allSprites[4].AddSequence("PinkyUp", 8, 2, 150);
            _allSprites[5].AddSequence("PinkyDown", 10, 2, 150);
            _allSprites[6].AddSequence("PinkyLeft", 12, 2, 150);
            _allSprites[7].AddSequence("PinkyRight", 14, 2, 150);

            _allSprites[8].AddSequence("InkyUp", 16, 2, 150);
            _allSprites[9].AddSequence("InkyDown", 18, 2, 150);
            _allSprites[10].AddSequence("InkyLeft", 20, 2, 150);
            _allSprites[11].AddSequence("InkyRight", 22, 2, 150);

            _allSprites[12].AddSequence("ClydeUp", 24, 2, 150);
            _allSprites[13].AddSequence("ClydeDown", 26, 2, 150);
            _allSprites[14].AddSequence("ClydeLeft", 28, 2, 150);
            _allSprites[15].AddSequence("ClydeRight", 30, 2, 150);

            _allSprites[16].AddSequence("Frightened", 32, 2, 150);
            _allSprites[17].AddSequence("FrightenedFlash", 32, 4, 300);
            _allSprites[18].AddSequence("FrightenedFastFlash", new[] { 32, 35 }, 150);

            _allSprites[19].AddSequence("PacmanChomp", new[] { 36, 37, 36, 38 }, 150);
            _allSprites[20].AddSequence("PacmanDie", 39, 11, 825);

            _allSprites[21].AddSequence("EyesUp", 50, 1, 1000);
            _allSprites[22].AddSequence("EyesDown", 51, 1, 1000);
            _allSprites[23].AddSequence("EyesLeft", 52, 1, 1000);
            _allSprites[24].AddSequence("EyesRight", 53, 1, 1000);

            _allSprites[25].AddSequence("InnerWall", 54, 1, 1000);
            _allSprites[26].AddSequence("InnerWallCorner", 55, 1, 1000);
            _allSprites[27].AddSequence("InnterWallCorner2", 58, 1, 1000);
            _allSprites[28].AddSequence("OuterWall", 56, 1, 1000);
            _allSprites[29].AddSequence("OuterWallCorner", 57, 1, 1000);

            _allSprites[30].AddSequence("Pellet", 60, 1, 1000);
            _allSprites[31].AddSequence("PowerPellet", 59, 1, 1000);

            var itemsPerRow = 16;
            var spacing = 40;
            for (var i = 0; i < _allSprites.Count; i++)
                _allSprites[i].Translate((i % itemsPerRow) * spacing, ((float)Math.Floor((decimal)i / itemsPerRow) * spacing));
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
                Stage.Game.Exit();
            }
        }
    }
}
