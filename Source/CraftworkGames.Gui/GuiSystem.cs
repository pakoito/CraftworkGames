
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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.IO;
using CraftworkGames.Core;

namespace CraftworkGames.Gui
{
    public class GuiSystem
    {   
        public GuiSystem(IDrawManager drawManager, IInputManager inputManager, 
            int virtualScreenWidth = 800, int virtualScreenHeight = 480)
        {
            VirtualScreenWidth = virtualScreenWidth;
            VirtualScreenHeight = virtualScreenHeight;
            _drawManager = drawManager;
            _inputManager = inputManager;
        }

        public int VirtualScreenWidth { get; set; }
        public int VirtualScreenHeight { get; set; }

        private ILayoutControl _rootLayout;
        public ILayoutControl RootLayout 
        {
            get
            {
                return _rootLayout;
            }
            set
            {                
                _rootLayout = value;

                if (_rootLayout != null)
                {
                    _rootLayout.Width = VirtualScreenWidth;
                    _rootLayout.Height = VirtualScreenHeight;
                    _rootLayout.PerformLayout();
                }
            }
        }

        private IDrawManager _drawManager;
        private IInputManager _inputManager;

        private Vector2 GetScreenScale()
        {
            var scaleX = (float)_drawManager.ViewportWidth / VirtualScreenWidth;
            var scaleY = (float)_drawManager.ViewportHeight / VirtualScreenHeight;

            return new Vector2(scaleX, scaleY);
        }

        public bool Update(float deltaTime)
        {
            if (RootLayout != null)
            {
                var screenScale = GetScreenScale();

                _inputManager.ReadInputState(screenScale.X, screenScale.Y);

                foreach (var control in RootLayout.GetControls())
                    control.Update(_inputManager, deltaTime);
            }

            return true;
        }
                
        public void Draw()
        {
            var screenScale = GetScreenScale();

            if(RootLayout != null)
                _drawManager.Draw(screenScale.X, screenScale.Y, RootLayout.GetControls());
        }
    }
}

