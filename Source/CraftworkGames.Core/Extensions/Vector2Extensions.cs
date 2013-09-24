using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CraftworkGames.Core
{
    public static class Vector2Extensions
    {
        public static Vector3 ToVector3(this Vector2 vector2)
        {
            return new Vector3(vector2.X, vector2.Y, 0);
        }

        public static Vector2 ToVector2(this Point point)
        {
            return new Vector2(point.X, point.Y);
        }
    }
    
}
