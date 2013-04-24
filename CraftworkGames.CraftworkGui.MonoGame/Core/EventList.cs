using System;
using System.Collections.Generic;

namespace CraftworkGames.CraftworkGui.MonoGame
{
    public class ItemEventArgs<T> : EventArgs
    {
        public ItemEventArgs(T item)
        {
            Item = item;
        }

        public T Item { get; private set; }
    }

    public delegate void ItemEventHandler<T>(object sender, ItemEventArgs<T> e);

    public class EventList<T> : IList<T>
    {
        public EventList()
        {
        }

        public event ItemEventHandler<T> ItemAdded;
        public event ItemEventHandler<T> ItemRemoved;

        private void RaiseEvent(ItemEventHandler<T> eventHandler, T item)
        {
            if(eventHandler != null)
                eventHandler(this, new ItemEventArgs<T>(item));
        }

        #region IList implementation

        private List<T> _list = new List<T>();

        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            RaiseEvent(ItemAdded, item);
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            RaiseEvent(ItemRemoved, _list[index]);
            _list.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return _list[index];
            }
            set
            {
                _list[index] = value;
            }
        }

        #endregion

        #region ICollection implementation

        public void Add(T item)
        {
            RaiseEvent(ItemAdded, item);
            _list.Add(item);
        }

        public void Clear()
        {
            foreach(var item in _list)
                RaiseEvent(ItemRemoved, item);

            _list.Clear();
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if(_list.Remove(item))
            {
                RaiseEvent(ItemRemoved, item);
                return true;
            }

            return false;
        }

        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        #endregion

        #region IEnumerable implementation

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion

        #region IEnumerable implementation

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion
    }
}

