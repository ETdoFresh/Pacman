using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace DisplayLibrary
{
    public class GroupObject : DisplayObject
    {
        private List<DisplayObject> children = new List<DisplayObject>();

        public GroupObject(Position position = null, Rotation rotation = null, Scale scale = null, GroupObject parent = null)
            : base(parent, position, null, rotation, scale) { }

        public void Insert(DisplayObject child)
        {
            children.Add(child);
        }

        public void Remove(DisplayObject child, Boolean runDipose = true)
        {
            children.Remove(child);
            child.Parent = null;

            if (runDipose) 
                child.Dispose();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var child in children)
                if (child.IsVisible)
                    child.Draw(spriteBatch);
        }
    }
}
