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
        List<AnimatedSpriteObject> _allSprites;
        public AnimationTest()
            : base("AnimationTest")
        {
            _allSprites = new List<AnimatedSpriteObject>();

            Translate(30, 30);

            for (var i = 0; i < 8; i++)
            {
                var sprite = new AnimatedSpriteObject("pacman");
                _allSprites.Add(sprite);
                AddChild(sprite);
                sprite.Resize(0.75f);
            }

            _allSprites[0].AddSequence("BlinkyUp", 1, 8, 150);
            _allSprites[0].Tint = Color.Red;

            _allSprites[1].AddSequence("PinkyUp", 1, 8, 150);
            _allSprites[1].Tint = Color.Pink;
            
            _allSprites[2].AddSequence("InkyUp", 1, 8, 150);
            _allSprites[2].Tint = Color.Cyan;
            
            _allSprites[3].AddSequence("ClydeUp", 1, 8, 150);
            _allSprites[3].Tint = Color.Orange;

            _allSprites[4].AddSequence("Frightened", 1, 8, 150);
            _allSprites[4].Tint = Color.Blue;
            
            _allSprites[5].AddSequence("PacmanChomp", new[] { 9, 10, 11, 12,13,14,15,16,15,14,13,12,11,10 }, 150);
            _allSprites[5].Tint = Color.Yellow;
                        
            _allSprites[6].AddSequence("Pellet", 0, 1, 1000);

            _allSprites[7].AddSequence("PowerPellet", 17, 1, 1000);

            var itemsPerRow = 16;
            var spacing = 75;
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
