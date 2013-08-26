﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pacman.Engine.Display
{
    class GroupObject : DisplayObject
    {
        List<GameObject> Children;

        public GroupObject()
        {
            Children = new List<GameObject>();
        }

        public GameObject this[int index]
        {
            get { return Children[index]; }
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (var child in Children)
                if (!child.IsInitialized)
                    child.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var child in Children)
                if(child.Enabled)
                    child.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (var child in Children)
                if (child.Visible)
                    child.Draw(gameTime);
        }

        public GameObject AddChild(GameObject child)
        {
            Children.Remove(child);
            Children.Add(child);
            child.Parent = this;
            return child;
        }

        public GameObject RemoveChild(GameObject child)
        {
            Children.Remove(child);
            child.Parent = this;
            return child;
        }
    }
}
