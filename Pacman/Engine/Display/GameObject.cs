using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Pacman.Engine.Display
{
    class GameObject
    {
        List<GameObject> ComponentList;

        public GameObject()
        {
            Enabled = true;
            Visible = true;
            Debug.WriteLine("Game Object Constructed | " + this);
            ComponentList = new List<GameObject>();

            if (Stage.IsInitialized)
                Initialize();
        }

        public virtual void Initialize() 
        {
            IsInitialized = true;
            foreach (var component in ComponentList)
                if (!component.IsInitialized)
                    component.Initialize();

            Debug.WriteLine("Game Object Initalized | " + this);
            LoadContent();
        }

        public virtual void LoadContent() 
        {
            Debug.WriteLine("Game Object Content Loaded | " + this);
        }

        public virtual void UnloadContent() 
        {
            Debug.WriteLine("Game Object Content Unloaded | " + this);
        }

        public virtual void Update(GameTime gameTime) 
        {
            // Copy List in case list gets modified outside of loop
            var CopyOfComponents = new List<GameObject>(ComponentList);
            foreach (var component in CopyOfComponents)
                if (component.Enabled)
                    component.Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime) {
            foreach (var component in ComponentList)
                if (component.Visible)
                    component.Draw(gameTime);
        }

        public virtual void RemoveSelf()
        {
            if (Parent != null)
                Parent.RemoveComponent(this, false);
        }

        public virtual GameObject AddComponent(GameObject component)
        {
            if (component.Parent != null)
                component.Parent.RemoveComponent(component, false);

            ComponentList.Add(component);
            component.Parent = this;
            return component;
        }

        public virtual GameObject RemoveComponent(GameObject component, bool RunRemoveSelf)
        {
            ComponentList.Remove(component);
            component.Parent = null;
            if (RunRemoveSelf) component.RemoveSelf();
            return component;
        }

        public virtual GameObject RemoveComponent(GameObject child)
        { 
            return RemoveComponent(child, true);
        }

        public bool IsInitialized { get; protected set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public GameObject Parent { get; set; }

        public GameObject this[int index]
        {
            get { return ComponentList[index]; }
        }
    }
}
