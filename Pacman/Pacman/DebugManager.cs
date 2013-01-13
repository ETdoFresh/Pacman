using Pacman.DisplayObject;
using System;
using Microsoft.Xna.Framework;

namespace Pacman
{
    static class DebugManager
    {
        public static TextObject textObject = display.NewText("");
        public static RectangleObject rectangle = display.NewRect(0,0,1,1);
        public static Player Player { get; set; }
        public static Ghost Ghost { get; set; }
        public static Map Map { get; set; }

        public static void Update()
        {
            textObject.Text = "";
            if (Player != null)
            {
                var tileX = (int)Math.Floor(Player.X / Map.TileWidth);
                var tileY = (int)Math.Floor(Player.Y / Map.TileHeight);
                textObject.Text += string.Format("Player.X = {0}\n", Player.X);
                textObject.Text += string.Format("Player.Y = {0}\n", Player.Y);
                textObject.Text += string.Format("Player.tileX = {0}\n", tileX);
                textObject.Text += string.Format("Player.tileY = {0}\n", tileY);
                rectangle.Color = Color.Yellow;
                rectangle.Width = Map.TileWidth;
                rectangle.Height = Map.TileHeight;
                rectangle.X = tileX * Map.TileWidth;
                rectangle.Y = tileY * Map.TileHeight;
            }
            if (Ghost != null)
            {
                textObject.Text += string.Format("Ghost.X = {0}\n", Ghost.X);
                textObject.Text += string.Format("Ghost.Y = {0}\n", Ghost.Y);
            }
            textObject.X = textObject.ContentWidth / 2;
            textObject.Y = textObject.ContentHeight / 2;
        }
    }
}
