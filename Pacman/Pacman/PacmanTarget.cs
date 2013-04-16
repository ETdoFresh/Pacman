using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;
using Microsoft.Xna.Framework;

namespace Pacman
{
    class PacmanTarget : GameObject
    {
        public RectangleObject Graphic { get; set; }

        public PacmanTarget()
        {
            Position = new Position();
            TilePosition = new TilePosition(Position);

            var dimension = new Dimension(TileEngine.TileWidth, TileEngine.TileHeight);
            Graphic = new RectangleObject(parent: DisplayParent, position: Position, dimension: dimension);
            disposables.Add(Graphic);
        }

        public void UpdatePacmanTarget(GameTime gameTime)
        {
        }

        public override void Dispose()
        {
            Runtime.GameUpdate -= UpdatePacmanTarget;
            base.Dispose();
        }
    }
}
