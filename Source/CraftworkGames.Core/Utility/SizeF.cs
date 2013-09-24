using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CraftworkGames.Core
{
    public struct SizeF
    {
        public SizeF(float width, float height)
            : this()
        {
            Width = width;
            Height = height;
        }

        public float Width { get; private set; }
        public float Height { get; private set; }

        public Vector2 ToVector2()
        {
            return new Vector2(Width, Height);
        }
    }	
}
