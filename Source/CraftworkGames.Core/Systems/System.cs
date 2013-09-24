using System;
using System.Collections.Generic;

namespace CraftworkGames.Core
{
    public abstract class System<T> : ISystem
    {
        public System()
        {
        }

        public void AddComponent(IEntityComponent component)
        {
            if (component is T)
                AddComponent((T)component);
        }

        public void RemoveComponent(IEntityComponent component)
        {
            if (component is T)
                RemoveComponent((T)component);
        }

        public abstract void AddComponent(T component);
        public abstract void RemoveComponent(T component);
    }
}

