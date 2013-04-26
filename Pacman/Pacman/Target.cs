using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;

namespace Pacman
{
    abstract class Target : GameObject
    {
        protected RectangleObject rectangleGraphic;
        protected GameObject source;

        public Target(GameObject source, GroupObject displayParent = null)
        {
            this.source = source;
            Position = new Position();
            TilePosition = new TilePosition(Position);

            var dimension = new Dimension(TileEngine.TileWidth, TileEngine.TileHeight);
            rectangleGraphic = new RectangleObject(parent: displayParent, position: Position, dimension: dimension);
            disposables.Add(rectangleGraphic);
        }
    }
}
