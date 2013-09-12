using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pacman.Engine.Helpers;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman.Engine.Display
{
    /// <summary>
    /// A GameObject that can be displayed on screeen
    /// </summary>
    class DisplayObject : GameObject
    {
        public DisplayObject DisplayParent { get; set; }
        public Position Position { get; set; }
        public Orientation Orientation { get; set; }
        public Scale Scale { get; set; }
        public Vector2 Origin { get; protected set; }

        public float Alpha { get; set; }
        public Color Tint { get; set; }
        public virtual float Width { get; set; }
        public virtual float Height { get; set; }

        public Vector2 ContentPosition { get { return DisplayParent != null ? DisplayParent.ContentPosition + Position.Value * DisplayParent.ContentScale : Position.Value; } }
        public float ContentOrientation { get { return DisplayParent != null ? DisplayParent.ContentOrientation + Orientation.Value : Orientation.Value; } }
        public float ContentScale { get { return DisplayParent != null ? DisplayParent.ContentScale * Scale.Value : Scale.Value; } }
        public float ContentWidth { get { return Width * ContentScale; } }
        public float ContentHeight { get { return Height * ContentScale; } }

        public DisplayObject()
            : base()
        {
            Position = new Position(0, 0);
            Orientation = new Orientation(0);
            Scale = new Scale(1);
            Alpha = 1;
            Tint = Color.White;
        }

        /// <summary>
        /// Sets self as DisplayParent if component is a DisplayObject
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public override GameObject AddComponent(GameObject component)
        {
            if (component is DisplayObject)
                (component as DisplayObject).DisplayParent = this;
            return base.AddComponent(component);
        }

        /// <summary>
        /// Removes self as DisplayParent if component is a DisplayObject
        /// </summary>
        /// <param name="component"></param>
        /// <param name="RunRemoveSelf"></param>
        /// <returns></returns>
        public override GameObject RemoveComponent(GameObject component, bool RunRemoveSelf)
        {
            if (component is DisplayObject)
                (component as DisplayObject).DisplayParent = null;
            return base.RemoveComponent(component, RunRemoveSelf);
        }

        /// <summary>
        /// Change Position Value of DisplayObject
        /// </summary>
        /// <param name="x">Local X Coordinate</param>
        /// <param name="y">Local Y Coordinate</param>
        public virtual void Translate(float x, float y)
        {
            Position.X = x;
            Position.Y = y;
        }

        /// <summary>
        /// Change Position Value of DisplayObject
        /// </summary>
        /// <param name="position">Local Vector2 Coordinate</param>
        public virtual void Translate(Position position)
        {
            Position.X = position.X;
            Position.Y = position.Y;
        }

        /// <summary>
        /// Set Orientation Value of DisplayObject
        /// </summary>
        /// <param name="degrees">Degrees of orientation</param>
        public virtual void Rotate(float degrees)
        {
            Orientation.Value = MathHelper.ToRadians(degrees);
        }


        /// <summary>
        /// Resizes DisplayObject
        /// </summary>
        /// <param name="value">Percent value</param>
        public virtual void Resize(float value)
        {
            Scale.Value = value;
        }

        /// <summary>
        /// Sets Origin Point of DisplayObject
        /// </summary>
        /// <param name="x">Local X Coordinate</param>
        /// <param name="y">Local Y Coordinate</param>
        public virtual void SetOrigin(float x, float y)
        {
            Origin = new Vector2(x, y);
        }
    }
}
