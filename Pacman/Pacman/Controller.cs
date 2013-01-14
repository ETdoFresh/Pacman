using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pacman.DisplayEngine;

namespace Pacman
{
    class Controller
    {
        private Map map;
        private Player player;
        private Ghost ghost;
        private List<DisplayObject> objects;

        public Controller(Map map, Player player, Ghost ghost)
        {
            this.map = map;
            this.player = player;
            this.ghost = ghost;
            this.objects = new List<DisplayObject>() { player, ghost };
        }

        public void Update(GameTime gameTime)
        {
            foreach (var obj in objects)
            {
                wrapAroundMap(obj);
            }
        }

        private void wrapAroundMap(DisplayObject obj)
        {
            if (obj.X < 0)
                obj.X = map.Width - 1;
            else if (obj.X >= map.Width)
                obj.X = 0;
            if (obj.Y < 0)
                obj.Y = map.Height - 1;
            else if (obj.Y >= map.Height)
                obj.Y = 0;
        }
    }
}
