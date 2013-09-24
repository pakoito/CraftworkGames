using System;
using Microsoft.Xna.Framework;

namespace CraftworkGames.Core
{
    public interface ITransformable : IMoveable, IRotatable, IEntityComponent
    {
        Guid Id { get; }
    }
}
