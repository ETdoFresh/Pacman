using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman.DisplayObject
{
    class DisplayObject : EventListener
    {
        public float Alpha { get; set; }
        public float Height { get; set; }
        public bool IsVisible { get; set; }
        public GroupObject Parent { get; set; }
        public float Orientation { get; set; }
        public float Width { get; set; }
        public float X { get { return Position.X; } set { Position = new Vector2(value, Position.Y); } }
        public float XOrigin { get; set; }
        public float XReference { get; set; }
        public float XScale { get; set; }
        public float Y { get { return Position.Y; } set { Position = new Vector2(Position.X, value); } }
        public float YOrigin { get; set; }
        public float YReference { get; set; }
        public float YScale { get; set; }
        public Color Color { get; set; }

        public float ContentX { get { return (Parent != null ? X * ContentXScale + Parent.ContentX : X * ContentXScale); } }
        public float ContentY { get { return (Parent != null ? Y * ContentYScale + Parent.ContentY : Y * ContentYScale); } }
        public float ContentOrientation { get { return Parent != null ? Orientation + Parent.ContentOrientation : Orientation; } }
        public float ContentXScale { get { return Parent != null ? XScale * Parent.ContentXScale : XScale; } }
        public float ContentYScale { get { return Parent != null ? YScale * Parent.ContentYScale : YScale; } }
        public float ContentWidth { get { return Width * ContentXScale; } }
        public float ContentHeight { get { return Height * ContentXScale; } }

        public Vector2 Position { get; set; }

        public DisplayObject()
        {
            X = 0;
            Y = 0;
            Orientation = 0;
            XScale = 1;
            YScale = 1;
            IsVisible = true;
            Alpha = 1;
            Color = Color.White;
        }

        public Vector2 ContentToLocal(Vector2 contentPosition)
        {
            if (Parent != null)
                return Parent.ContentToLocal(contentPosition - Position);

            return contentPosition;
        }

        public Vector2 LocalToContent(Vector2 localPosition)
        {
            if (Parent != null)
                return Parent.LocalToContent(localPosition + Position);

            return localPosition;
        }

        public void RemoveSelf()
        {
            if (Parent != null)
                Parent.Remove(this);
        }

        public void Rotate(float deltaAngle)
        {
            Orientation += MathHelper.ToRadians(deltaAngle);
        }

        public void Scale(Vector2 scale)
        {
            XScale *= scale.X;
            YScale *= scale.Y;
        }

        public void ToBack()
        {
            if (Parent != null)
                Parent.Insert(this);
        }

        public void ToFront()
        {
            if (Parent != null)
                Parent.Insert(this);
        }

        public void Translate(Vector2 delta)
        {
            X += delta.X;
            Y += delta.Y;
        }

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
