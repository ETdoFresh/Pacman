using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Pacman.Engine.Helpers
{
    public enum MouseButton { Left, Right, Middle, }
    public enum InputState { Pressed, Hold, Released, Idle, }

    static class InputHelper
    {
        static private KeyboardState _previousKeyboardState;
        static private KeyboardState _currentKeyboardState;

        static private MouseState _previousMouseState;
        static private MouseState _currentMouseState;

        static public float MouseX { get { return _currentMouseState.X; } }
        static public float MouseY { get { return _currentMouseState.Y; } }
        static public Vector2 MousePos { get { return new Vector2(_currentMouseState.X, _currentMouseState.Y); } }

        static InputHelper()
        {
            _currentKeyboardState = Keyboard.GetState();
            _currentMouseState = Mouse.GetState();
        }

        static public void Update()
        {
            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
        }

        static public InputState GetInputState(Keys key)
        {
            if (_previousKeyboardState.IsKeyUp(key) && _currentKeyboardState.IsKeyDown(key))
                return InputState.Pressed;
            else if (_previousKeyboardState.IsKeyDown(key) && _currentKeyboardState.IsKeyDown(key))
                return InputState.Hold;
            else if (_previousKeyboardState.IsKeyDown(key) && _currentKeyboardState.IsKeyUp(key))
                return InputState.Released;
            else
                return InputState.Idle;
        }

        static public InputState GetInputState(MouseButton mouseButton)
        {
            ButtonState previous, current;
            switch (mouseButton)
            {
                case MouseButton.Left:
                default:
                    previous = _previousMouseState.LeftButton;
                    current = _currentMouseState.LeftButton;
                    break;
                case MouseButton.Middle:
                    previous = _previousMouseState.MiddleButton;
                    current = _currentMouseState.MiddleButton;
                    break;
                case MouseButton.Right:
                    previous = _previousMouseState.RightButton;
                    current = _currentMouseState.RightButton;
                    break;
            }

            if (previous == ButtonState.Released && current == ButtonState.Pressed)
                return InputState.Pressed;
            else if (previous == ButtonState.Pressed && current == ButtonState.Pressed)
                return InputState.Hold;
            else if (previous == ButtonState.Pressed && current == ButtonState.Released)
                return InputState.Released;
            else
                return InputState.Idle;
        }

        static public bool IsPressed(Keys key)
        {
            if (GetInputState(key) == InputState.Pressed)
                return true;

            return false;
        }

        static public bool IsPressed(MouseButton mouseButton)
        {
            if (GetInputState(mouseButton) == InputState.Pressed)
                return true;

            return false;
        }
    }
}
