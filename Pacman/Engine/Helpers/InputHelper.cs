using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Pacman.Engine.Helpers
{
    class InputHelper : GameObject
    {
        private KeyboardState _previousKeyboardState;
        private KeyboardState _currentKeyboardState;

        public InputHelper()
        {
            _currentKeyboardState = Keyboard.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        public bool IsPressed(Keys key)
        {
            if (_previousKeyboardState.IsKeyUp(key) && _currentKeyboardState.IsKeyDown(key))
                return true;

            return false;
        }
    }
}
