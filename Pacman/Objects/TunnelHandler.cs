using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pacman.Engine.Display;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework;

namespace Pacman.Objects
{
    class TunnelChecker : GameObject
    {
        static private int _tunnelY = 14;
        static private int _tunnelXLeft = 5;
        static private int _tunnelXRight = 22;

        public delegate void TunnelHandler();
        public event TunnelHandler TunnelStart = delegate { };
        public event TunnelHandler TunnelEnd = delegate { };

        TilePosition _tilePosition;
        bool _isTunneling;

        public TunnelChecker(Engine.Helpers.TilePosition tilePosition)
        {
            _tilePosition = tilePosition;
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                base.Update(gameTime);

                bool prevTunneling = _isTunneling;
                if (_tilePosition.Y == _tunnelY && (_tilePosition.X <= _tunnelXLeft || _tilePosition.X >= _tunnelXRight))
                    _isTunneling = true;
                else
                    _isTunneling = false;

                if (prevTunneling != _isTunneling)
                {
                    if (!prevTunneling)
                        TunnelStart();
                    else
                        TunnelEnd();
                }
            }
        }

        public override void RemoveSelf()
        {
            base.RemoveSelf();
            TunnelStart = null;
            TunnelEnd = null;
        }
    }
}
