using Pacman.DisplayObject;

namespace Pacman
{
    static class DebugManager
    {
        public static TextObject textObject = display.NewText("");
        public static Player Player { get; set; }
        public static Ghost Ghost { get; set; }

        public static void Update()
        {
            textObject.Text = "";
            if (Player != null)
            {
                textObject.Text += string.Format("Player.X = {0}\n", Player.X);
                textObject.Text += string.Format("Player.Y = {0}\n", Player.Y);
            }
            if (Ghost != null)
            {
                textObject.Text += string.Format("Ghost.X = {0}\n", Ghost.X);
                textObject.Text += string.Format("Ghost.Y = {0}\n", Ghost.Y);
            }
            textObject.X = textObject.ContentWidth / 2;
            textObject.Y = textObject.ContentHeight / 2;
        }
    }
}
