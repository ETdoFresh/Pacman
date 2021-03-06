﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class Pellets : DisplayObject
    {
        TileGrid _tileGrid;
        List<Pellet> _pelletList;

        public Pellets(TileGrid tileGrid)
        {
            _tileGrid = tileGrid;
            _pelletList = new List<Pellet>();
        }

        public Pellet AddPellet()
        {
            Pellet pellet = new Pellet(_tileGrid);
            AddComponent(pellet);
            _pelletList.Add(pellet);
            return pellet;
        }

        public Pellet AddPowerPellet()
        {
            PowerPellet powerPellet = new PowerPellet(_tileGrid);
            AddComponent(powerPellet);
            _pelletList.Add(powerPellet);
            return powerPellet;
        }

        public Pellet RemovePellet(Pellet pellet)
        {
            _pelletList.Remove(pellet);
            pellet.RemoveSelf();
            return pellet;
        }

        public Pellet GetPellet(int index)
        {
            return _pelletList[index]; 
        }

        public int NumPellets { get { return _pelletList.Count; } }

        public class Pellet : SpriteObject
        {
            public Pellet(TileGrid tileGrid)
                : base("pacman", 26)
            {
                TilePosition = new TilePosition(Position, tileGrid.TileWidth, tileGrid.TileHeight);
                AddComponent(TilePosition);
            }

            public TilePosition TilePosition { get; set; }
        }

        public class PowerPellet : Pellet
        {
            const int _flashRate = 167;
            int _elapsedTime;

            public PowerPellet(TileGrid tileGrid)
                : base(tileGrid)
            {
                ChangeIndex(27);
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
                Flash(gameTime.ElapsedGameTime.Milliseconds);
            }

            private void Flash(int elapsedTime)
            {
                _elapsedTime += elapsedTime;
                if (_elapsedTime >= _flashRate)
                {
                    Visible = !Visible;
                    _elapsedTime = 0;
                }
            }
        }
    }
}
