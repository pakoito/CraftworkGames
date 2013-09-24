using System;
using Microsoft.Xna.Framework;
using CraftworkGames.Core;

namespace CraftworkGames.Gui
{
    public class TextStyle
    {
        public TextStyle()
        {
            Colour = Color.White;
            HorizontalAlignment = HorizontalAlignment.Centre;
            VerticalAlignment = VerticalAlignment.Centre;
        }

        public Color Colour { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }

//        public void Draw(IDrawManager drawManager, string text, Rectangle destinationRectangle)
//        {
//            if(!string.IsNullOrEmpty(text))
//                drawManager.DrawText(text, destinationRectangle, this);
//        }
    }
}

