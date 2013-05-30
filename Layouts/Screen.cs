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

using System;
using System.Collections.Generic;
using CraftworkGames.Core;
using Microsoft.Xna.Framework;

namespace CraftworkGames.Gui
{
    public abstract class Screen : LayoutControl<Control>, IUpdate, IDraw, IRectangular
    {
        public Screen(int width = 800, int height = 480)
        {
            Width = width;
            Height = height;
            IsInitialised = false;
            Items.ItemAdded += ItemAdded;
        }

        private void ItemAdded (object sender, ItemEventArgs<Control> e)
        {
            PerformLayout();
        }

        public ScreenManager ScreenManager { get; private set; }
        public IGuiSprite Background { get; set; }

        // hiding the public setters from the base class
        public new int X { get { return 0; } }
        public new int Y { get { return 0; } }
        public new int Width { get; private set; }
        public new int Height { get; private set; }
        public new Rectangle Rectangle { get { return new Rectangle(X, Y, Width, Height); } }

        public bool IsInitialised { get; private set; }

        public void Initialise(ScreenManager screenManager)
        {
            if (!IsInitialised)
            {
                ScreenManager = screenManager;
                OnInitialised(screenManager);
                IsInitialised = true;
            }
        }

        public abstract void OnInitialised(ScreenManager screenManager);

        public bool IsActive { get; private set; }
        public event EventHandler Activated;
        public event EventHandler Deactivated;

        public void Activate()
        {
            IsActive = true;

            if (Activated != null)
                Activated(this, EventArgs.Empty);
        }

        public void Deactivate()
        {
            IsActive = false;

            if(Deactivated != null)
                Deactivated(this, EventArgs.Empty);
        }

        public override void PerformLayout()
        {
            foreach(var control in Items)
                AlignControl(control, this);
        }

        protected override IEnumerable<Control> GetControls()
        {
            return Items;
        }

        public override void Draw(IDrawManager drawManager)
        {
            drawManager.StartBatch();
  
            if(Background != null)
                drawManager.Draw(Background, Rectangle);

            base.Draw(drawManager);

            drawManager.EndBatch();
        }
    }
}

