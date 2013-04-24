#region License
/*
MIT License
Copyright © 2013 Craftwork Games

All rights reserved.

Authors:
Dylan Wilson

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion License

using System;
using System.Collections.Generic;

namespace CraftworkGames.CraftworkGui.MonoGame
{
    public class GuiLayer : IUpdate, IDraw, IRectangle
    {
        public GuiLayer(int width, int height)
        {
            Width = width;
            Height = height;
            Controls = new EventList<Control>();
            Controls.ItemAdded += Controls_ItemAdded;
        }

        private void Controls_ItemAdded (object sender, ItemEventArgs<Control> e)
        {
            var layoutControl = e.Item as LayoutControl;

            if(layoutControl != null)
                layoutControl.PerformLayout();
        }

        public int X { get { return 0; } }
        public int Y { get { return 0; } }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public string BackgroundName { get; set; }
        public EventList<Control> Controls { get; private set; }

        public void Update(IUpdateManager updateManager, float deltaTime)
        {
            foreach(var control in Controls)
                control.Update(updateManager, deltaTime);
        }
        
        public void Draw(IDrawManager drawManager)
        {
            drawManager.StartBatch();

            if(!string.IsNullOrEmpty(BackgroundName))
                drawManager.DrawTexture(BackgroundName, this);

            foreach(var control in Controls)
                control.Draw(drawManager);

            drawManager.EndBatch();
        }
    }
}

