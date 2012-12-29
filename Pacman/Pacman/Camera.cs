using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman
{
    public static class Camera
    {
        public static Vector2 Position { get { return position; } set { position = value; } }
        private static Vector2 position;

        public static Vector2 GetWorldCoordinatesFromScreenCoordinates(Vector2 screenCoordinates)
        {
            var worldCoordinates = screenCoordinates - position;
            return worldCoordinates;
        }

        public static Vector2 GetScreenCoordinatesFromWorldCoordinates(Vector2 worldCoordinates)
        {
            var screenCoordinates = worldCoordinates + position;
            return screenCoordinates;
        }
    }
}
