using System;

namespace CraftworkGames.Gui
{
    public abstract class TextControl : Control
    {
        public TextControl(VisualStyle defaultStyle)
            : base(defaultStyle)
        {
            TextStyle = new TextStyle();
        }

        public string Text { get; set; }
        public TextStyle TextStyle { get; set; }
    }
}

