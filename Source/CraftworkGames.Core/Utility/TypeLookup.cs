using System;
using System.Linq;
using System.Collections.Generic;

namespace CraftworkGames.Core
{
    public class TypeLookup<TBase>
    {
        private Dictionary<Type, List<TBase>> _typeMap = new Dictionary<Type, List<TBase>>();

        public void Clear()
        {
            _typeMap.Clear();
        }

        public void Add<T>(TBase value)
        {
            var type = typeof(T);

            if (!(value is T))
                throw new ArgumentException(string.Format("{0} is not of type {1}", value, type), "value");

            if (_typeMap.ContainsKey(type))
            {
                _typeMap [type].Add(value);
            } 
            else
            {
                var list = new List<TBase>();
                list.Add(value);
                _typeMap.Add(type, list);
            }
        }

        public bool TryAdd<T>(TBase value)
        {
            var type = typeof(T);

            if (!(value is T))
                return false;

            Add<T>(value);
            return true;
        }

        public void Remove<T>(TBase value)
        {
            var type = typeof(T);

            if (_typeMap.ContainsKey(type))
            {
                _typeMap [type].Remove(value);
            }
        }

        public IEnumerable<T> GetForType<T>()
        {
            var type = typeof(T);
            List<TBase> list;

            if(_typeMap.TryGetValue(type, out list))
                return list.Cast<T>();

            return Enumerable.Empty<T>();
        }

        public IEnumerable<TBase> GetAll()
        {
            return _typeMap.SelectMany(i => i.Value);
        }
    }
    
}
