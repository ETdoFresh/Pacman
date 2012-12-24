using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pacman
{
    class PlayerManager
    {
        private Player player;

        public PlayerManager(Player player)
        {
            this.player = player;
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            UpdateVelocityFromKeyboard(keyboardState);
        }

        private void UpdateVelocityFromKeyboard(KeyboardState keyboardState)
        {
            var keyDictionary = new Dictionary<Keys, Vector2>
            {
                {Keys.Left, new Vector2(-1, 0)},
                {Keys.Right, new Vector2(1, 0)},
                {Keys.Up, new Vector2(0, -1)},
                {Keys.Down, new Vector2(0, 1)},
            };

            var velocity = Vector2.Zero;

            foreach (var key in keyDictionary)
                if (keyboardState.IsKeyDown(key.Key))
                    velocity += key.Value;

            player.Velocity = velocity;
        }
    }
}
