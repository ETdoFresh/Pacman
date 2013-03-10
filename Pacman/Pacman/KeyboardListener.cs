using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Pacman
{
    class KeyboardListener
    {
        public delegate void KeyboardEventHandler(Keys key); 
        static public event KeyboardEventHandler Press;
        static public event KeyboardEventHandler Release;

        static private List<Keys> pressedKeys = new List<Keys>();

        static public void Update(GameTime gameTime)
        {
            var keysDown = Keyboard.GetState().GetPressedKeys();
            UpdatePressedKeys(keysDown);
            UpdateReleasedKeys(keysDown);
        }

        static private void UpdateReleasedKeys(Keys[] keysDown)
        {
            for (var i = pressedKeys.Count - 1; i >= 0; i--)
            {
                var key = pressedKeys[i];
                if (!keysDown.Contains<Keys>(key))
                {
                    if (Release != null) Release(key);
                    pressedKeys.RemoveAt(i);
                }
            }
        }

        static private void UpdatePressedKeys(Keys[] keysDown)
        {
            foreach (var key in keysDown)
            {
                if (!pressedKeys.Contains(key))
                {
                    if (Press != null) Press(key);
                    pressedKeys.Add(key);
                }
            }
        }
    }
}
