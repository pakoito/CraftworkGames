using System;
using System.Collections.Generic;

namespace CraftworkGames.Gui
{
    public interface ILayoutControl : IUpdate, IDraw
    {
        IEnumerable<Control> GetControls();
        void PerformLayout();

        int Height { get; set; }

        int Width { get; set; }
    }
}

