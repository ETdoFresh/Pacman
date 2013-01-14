using Pacman.DisplayEngine;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pacman
{
    static class DebugManager
    {
        public static TextObject textObject = display.NewText("");
        public static List<RectangleObject> Rectangles { get; set; }
        public static Player Player { get; set; }
        public static Ghost Ghost { get; set; }
        public static Map Map { get; set; }

        public static void Update()
        {
            textObject.Text = "";
            if (Player != null)
            {
                var tile = Map.GetTilePositionFromChild(Player);
                textObject.Text += string.Format("Player.X = {0}\n", Player.X);
                textObject.Text += string.Format("Player.Y = {0}\n", Player.Y);
                textObject.Text += string.Format("Player.tileX = {0}\n", tile.X);
                textObject.Text += string.Format("Player.tileY = {0}\n", tile.Y);
                Rectangles[0].Color = Color.Yellow;
                Rectangles[0].X = tile.X * Map.TileWidth;
                Rectangles[0].Y = tile.Y * Map.TileHeight;
            }
            if (Ghost != null)
            {
                var tile = Map.GetTilePositionFromChild(Ghost);
                textObject.Text += string.Format("Ghost.X = {0}\n", Ghost.X);
                textObject.Text += string.Format("Ghost.Y = {0}\n", Ghost.Y);
                textObject.Text += string.Format("Ghost.tileX = {0}\n", tile.X);
                textObject.Text += string.Format("Ghost.tileY = {0}\n", tile.Y);
                Rectangles[1].Color = Color.Red;
                Rectangles[1].X = tile.X * Map.TileWidth;
                Rectangles[1].Y = tile.Y * Map.TileHeight;
            }
            textObject.X = textObject.ContentWidth / 2;
            textObject.Y = textObject.ContentHeight / 2;
        }
    }
}
