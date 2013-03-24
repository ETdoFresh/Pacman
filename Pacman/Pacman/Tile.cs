using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayLibrary;

namespace Pacman
{
    class Tile : GameObject
    {
        private GroupObject tileGroup = new GroupObject();
        private List<Sprite> sprites = new List<Sprite>();

        public Tile(float x = 0, float y = 0, String filename = null, Int32 index = 0)
        {
            Position = new Position(x, y);
            Rotation = new Rotation();
            tileGroup.Position = Position;
            tileGroup.Rotation = Rotation;

            if (filename != null)
                AddLayer(filename, index);
        }

        public void AddLayer(String filename, Int32 index, float rotation = 0)
        {
            var localRotation = new Rotation(rotation);
            sprites.Add(new Sprite(filename, index, parent: tileGroup, rotation: localRotation));
        }

        public void RemoveLayer(Int32 index = -1)
        {
            if (index == -1)
                index = sprites.Count - 1;

            if (index > 0)
            {
                sprites[index].Dispose();
                sprites.RemoveAt(index);
            }
        }

        public override void Dispose()
        {
            foreach (var sprite in sprites)
                sprite.Dispose();

            base.Dispose();
        }
    }
}
