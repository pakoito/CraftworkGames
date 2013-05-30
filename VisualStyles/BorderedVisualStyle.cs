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
using CraftworkGames.Core;
using Microsoft.Xna.Framework;

namespace CraftworkGames.Gui
{
    /// <summary>
    /// This is a work in progress, use at your own risk.
    /// </summary>
    public class BorderedVisualStyle : VisualStyle
    {
        public BorderedVisualStyle(int borderThickness)
            : base(null)
        {
            BorderThickness = borderThickness;
        }

        public ITextureRegion TopLeftRegion { get; set; }
        public ITextureRegion TopRegion { get; set; }
        public ITextureRegion TopRightRegion { get; set; }
        public ITextureRegion LeftRegion { get; set; }
        public ITextureRegion CentreRegion { get; set; }
        public ITextureRegion RightRegion { get; set; }
        public ITextureRegion BottomLeftRegion { get; set; }
        public ITextureRegion BottomRegion { get; set; }
        public ITextureRegion BottomRightRegion { get; set; }
        public int BorderThickness { get; set; }

//        public override void Draw(IDrawManager drawManager, Rectangle destinationRectangle)
//        {
//            var border = new Border(destinationRectangle, BorderThickness);
//
//            drawManager.Draw(TopLeftRegion, border.TopLeft);
//            drawManager.Draw(TopRegion, border.Top);
//            drawManager.Draw(TopRightRegion, border.TopRight);
//            drawManager.Draw(LeftRegion, border.Left);
//            drawManager.Draw(CentreRegion, border.Centre);
//            drawManager.Draw(RightRegion, border.Right);
//            drawManager.Draw(BottomLeftRegion, border.BottomLeft);
//            drawManager.Draw(BottomRegion, border.Bottom);
//            drawManager.Draw(BottomRightRegion, border.BottomRight);
//        }
    }
}

