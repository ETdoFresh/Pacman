using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace PacmanLibrary.Engine
{
    class Group : DisplayObject
    {
        private List<DisplayObject> Children { get; set; }

        public int numChildren { get { return Children.Count; } }

        public Group(Group parent = null)
            : base(parent)
        {
            Children = new List<DisplayObject>();
        }

        public DisplayObject AddChild(DisplayObject child)
        {
            Children.Add(child);
            return child;
        }

        public DisplayObject AddChildAt(DisplayObject child, int index)
        {
            Children.Insert(index, child);
            return child;
        }

        public bool Contains(DisplayObject child)
        {
            return Children.Contains(child);
        }

        public DisplayObject GetChildAt(int index)
        {
            return Children[index];
        }

        public DisplayObject GetChildByName(string name)
        {
            foreach (var child in Children)
                if (child.Name == name)
                    return child;

            return null;
        }

        public int GetChildIndex(DisplayObject child)
        {
            return Children.IndexOf(child);
        }

        public DisplayObject RemoveChild(DisplayObject child)
        {
            Children.Remove(child);
            return child;
        }

        public DisplayObject RemoveChildAt(int index)
        {
            var child = Children[index];
            Children.RemoveAt(index);
            return child;
        }

        public void RemoveChildren(int beginIndex, int endIndex = int.MaxValue)
        {
            if (endIndex == int.MaxValue) endIndex = Math.Min(endIndex, Children.Count);
            Children.RemoveRange(beginIndex, endIndex - beginIndex);
        }

        public void SetChildIndex(DisplayObject child, int index) 
        {
            Children.Remove(child);
            Children.Insert(index, child);
        }

        public void SwapChildren(DisplayObject child1, DisplayObject child2) 
        {
            var index1 = GetChildIndex(child1);
            var index2 = GetChildIndex(child2);
            Children[index1] = child2;
            Children[index2] = child1;
        }
        public void SwapChildrenAt(int index1, int index2) 
        {
            var child1 = GetChildAt(index1);
            var child2 = GetChildAt(index2);
            Children[index1] = child2;
            Children[index2] = child1;
        }

        public override void Initialize()
        {
            base.Initialize();
            foreach (var child in Children) child.Initialize();
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            foreach (var child in Children) child.LoadContent(Content);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            foreach (var child in Children) child.UnloadContent();
        }

        public override void Update(RenderContext renderContext)
        {
            base.Update(renderContext);
            foreach (var child in Children) child.Update(renderContext);
        }

        public override void Draw(RenderContext renderContext)
        {
            base.Update(renderContext);
            foreach (var child in Children) child.Draw(renderContext);
        }
    }
}
