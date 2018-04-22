using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Source.Core.Observable
{
    public delegate void OnAdd<T>(T added);

    public delegate void OnRemove<T>(T removed);

    public class ObservableList<T> : IEnumerable<T>
    {
        private IList<T> _list;

        private event OnAdd<T> _onAdd;
        /// <summary>
        /// Event that is called when an item is added.
        /// </summary>
        public event OnAdd<T> OnAdd
        {
            add {
                _onAdd += value;
            }
            remove { _onAdd -= value; }
        }

        private event OnRemove<T> _onRemove;
        /// <summary>
        /// Event that is called when an item is removed.
        /// </summary>
        public event OnRemove<T> OnRemove
        {
            add { _onRemove += value; }
            remove { _onRemove -= value; }
        }

        /// <summary>
        /// The amount of elements in the list.
        /// </summary>
        public int Count
        {
            get
            {
                if (_list == null) return 0;

                return _list.Count;
            }
        }

        /// <summary>
        /// The elements in the list.
        /// </summary>
        public T[] All
        {
            get
            {
                if (_list == null) return new T[0];

                return _list.ToArray();
            }
        }

        /// <summary>
        /// Add an element to the list.
        /// </summary>
        public void Add(T element)
        {
            if (_list == null) _list = new List<T>();

            _list.Add(element);
            _onAdd?.Invoke(element);
        }


        /// <summary>
        /// Remove an element from the list.
        /// </summary>
        public void Remove(T element)
        {
            if (_list == null || _list.Count == 0) return;

            _list.Remove(element);
            _onRemove?.Invoke(element);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
