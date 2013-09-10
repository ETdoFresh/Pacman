using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Pacman.Engine.Display
{
    /// <summary>
    /// The bread and butter of the game.
    /// It acts and smells like XNA component, but seperated from XNA logic.
    /// </summary>
    class GameObject
    {
        List<GameObject> _componentList;
        List<GameObject> _componentsToBeUpdated;

        public GameObject()
        {
            Enabled = true;
            Visible = true;
            Debug.WriteLine("Game Object Constructed | " + this);
            _componentList = new List<GameObject>();
            _componentsToBeUpdated = new List<GameObject>();

            if (Stage.IsInitialized)
                Initialize();
        }

        /// <summary>
        /// Initializes self and all components of self.
        /// Loads content when done.
        /// </summary>
        public virtual void Initialize() 
        {
            IsInitialized = true;
            foreach (var component in _componentList)
                if (!component.IsInitialized)
                    component.Initialize();

            Debug.WriteLine("Game Object Initalized | " + this);
            LoadContent();
        }

        /// <summary>
        /// Loads self. Super class does nothing, but subclasses will use this.
        /// Does not load components because previous initialize also loads components.
        /// </summary>
        public virtual void LoadContent() 
        {
            Debug.WriteLine("Game Object Content Loaded | " + this);
        }

        /// <summary>
        /// Unloads self and all components of self.
        /// </summary>
        public virtual void UnloadContent() 
        {
            foreach (var component in _componentList)
                component.UnloadContent();

            Debug.WriteLine("Game Object Content Unloaded | " + this);
        }

        /// <summary>
        /// Updates self and all components of self.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime) 
        {
            // Copy List in case list gets modified outside of loop
            _componentsToBeUpdated.Clear();
            _componentsToBeUpdated.AddRange(_componentList);
            foreach (var component in _componentsToBeUpdated)
                if (component.Enabled)
                    component.Update(gameTime);
        }

        /// <summary>
        /// Draws self and all components of self.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Draw(GameTime gameTime) {
            foreach (var component in _componentList)
                if (component.Visible)
                    component.Draw(gameTime);
        }

        /// <summary>
        /// Removes self and all components of self
        /// </summary>
        public virtual void RemoveSelf()
        {
            if (Parent != null)
                Parent.RemoveComponent(this, false);

            List<GameObject> componentsToBeRemoved = new List<GameObject>(_componentList);
            foreach (var component in componentsToBeRemoved)
                component.RemoveSelf();
        }

        /// <summary>
        /// Adds a component to self.
        /// </summary>
        /// <param name="component">Component to be added</param>
        /// <returns>Component that was added</returns>
        public virtual GameObject AddComponent(GameObject component)
        {
            if (component.Parent != null)
                component.Parent.RemoveComponent(component, false);

            _componentList.Add(component);
            component.Parent = this;
            return component;
        }

        /// <summary>
        /// Removes a component from self.
        /// </summary>
        /// <param name="component">Component to be removed</param>
        /// <param name="RunRemoveSelf">Should component run RemoveSelf</param>
        /// <returns>Component that was removed</returns>
        public virtual GameObject RemoveComponent(GameObject component, bool RunRemoveSelf)
        {
            _componentList.Remove(component);
            component.Parent = null;
            if (RunRemoveSelf) component.RemoveSelf();
            return component;
        }

        /// <summary>
        /// Removes component from self and component runs RemoveSelf().
        /// </summary>
        /// <param name="component">Component to be removed</param>
        /// <returns>Component that was removed</returns>
        public virtual GameObject RemoveComponent(GameObject component)
        { 
            return RemoveComponent(component, true);
        }

        public bool IsInitialized { get; protected set; }
        public bool Enabled { get; set; }
        public bool Visible { get; set; }
        public GameObject Parent { get; set; }

        public GameObject this[int index]
        {
            get { return _componentList[index]; }
        }
    }
}
