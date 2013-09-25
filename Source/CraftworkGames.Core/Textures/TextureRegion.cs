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
using Microsoft.Xna.Framework.Graphics;

namespace CraftworkGames.Core
{
    public class TextureRegion : ITextureRegion
    {
        public TextureRegion(Texture2D texture, int x, int y, int width, int height)
        {
            Texture = texture;
            TextureName = texture.Name;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public TextureRegion(Texture2D texture)
            : this(texture, 0, 0, texture.Width, texture.Height)
        {
        }

        public Texture2D Texture { get; private set; }
        public string TextureName { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(X, Y, Width, Height);
            }
        }
    }
    
}
