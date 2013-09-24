using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftworkGames.Core
{
    public interface ISprite
    {
        Color Colour { get; }
        float Rotation { get; }
        Vector2 Origin { get; }
        Vector2 Scale { get; }
        SpriteEffects Effect { get; }
        float Depth { get; set; }
    }    
}
