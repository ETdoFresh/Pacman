using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Pacman.DisplayObject
{
    static class display
    {
        // ------ Class Members ------ //
        public static ContentManager Content;
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, List<Rectangle>> rectangles = new Dictionary<string, List<Rectangle>>();
        public static GroupObject Stage = new GroupObject(null);
        public static SpriteFont font;

        // ------ Class Methods ------ //
        public static GroupObject NewGroup(GroupObject parent = null)
        {
            parent = parent == null ? Stage : parent;
            var newGroup = new GroupObject(parent);
            parent.Insert(newGroup);
            return newGroup;
        }

        public static ImageObject NewImage(GroupObject parent, string filename, int left = 0, int top = 0, bool isFullResolution = false)
        {
            parent = parent == null ? Stage : parent;
            var newImage = new ImageObject(RetrieveTexture(filename));
            parent.Insert(newImage);
            newImage.X += left + newImage.XOrigin;
            newImage.Y += top + newImage.YOrigin;
            return newImage;
        }
        public static ImageObject NewImage(string filename, int left = 0, int top = 0, bool isFullResolution = false)
        {
            return NewImage(Stage, filename, left, top, isFullResolution);
        }

        public static SpriteObject NewSprite(GroupObject parent, string filename)
        {
            parent = parent == null ? Stage : parent;
            var texture = RetrieveTexture(filename);
            var rectangles = RetrieveSourceRectangles(filename);
            if(rectangles.Count == 0)
                rectangles.Add(new Rectangle(0, 0, texture.Width, texture.Height));
            var newSprite = new SpriteObject(texture, rectangles);
            parent.Insert(newSprite);
            return newSprite;
        }
        public static SpriteObject NewSprite(string filename)
        {
            return NewSprite(Stage, filename);
        }

        public static TextObject NewText(GroupObject parent, string text, int left = 0, int top = 0)
        {
            parent = parent == null ? Stage : parent;
            var newText = new TextObject(text);
            parent.Insert(newText);
            newText.X = left + newText.XOrigin;
            newText.Y = top + newText.YOrigin;
            return newText;
        }
        public static TextObject NewText(string text, int left = 0, int top = 0)
        {
            return NewText(Stage, text, left, top);
        }

        public static Texture2D RetrieveTexture(string filename)
        {
            if (!textures.ContainsKey(filename))
                textures[filename] = Content.Load<Texture2D>(filename);

            return textures[filename];
        }
        public static List<Rectangle> RetrieveSourceRectangles(string filename)
        {
            if (!rectangles.ContainsKey(filename))
                rectangles[filename] = TexturePacker.GetTextureRectangles(Content.RootDirectory + "\\" + filename + ".xml");

            return rectangles[filename];
        }
    }
}
