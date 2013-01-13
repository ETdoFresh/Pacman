using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Pacman.DisplayObject
{
    class GroupObject : DisplayObject
    {
        private List<DisplayObject> children = new List<DisplayObject>();
        public int NumChildren { get { return children.Count; } }

        public GroupObject(GroupObject parent) : base() { }

        public void Remove(DisplayObject gameObject)
        {
            children.Remove(gameObject);
        }

        public void Remove(int index)
        {
            children.RemoveAt(index);
        }

        public void Insert(int index, DisplayObject child, bool resetTransform = false)
        {
            child.RemoveSelf();
            child.Parent = this;
            children.Insert(index, child);

            if (resetTransform)
            {
                child.X = 0;
                child.Y = 0;
                child.Orientation = 0;
                child.XScale = 1;
                child.YScale = 1;
            }
        }

        public void Insert(DisplayObject child, bool resetTransform = false)
        {
            Insert(children.Count, child, resetTransform);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (var child in children)
                child.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (var child in children)
                child.Update(gameTime);
        }
    }
}
