using System;
using System.Collections.Generic;

namespace CraftworkGames.Core
{
    public interface ISystem
    {
        void AddComponent(IEntityComponent component);
        void RemoveComponent(IEntityComponent component);
    }
}

