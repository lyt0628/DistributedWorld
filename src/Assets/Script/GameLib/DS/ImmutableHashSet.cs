using System.Collections;
using System.Collections.Generic;


namespace GameLib.DI
{

    class ImmutableHashSet<T> : ISet<T>
    {
        private readonly HashSet<T> _set;

        public ImmutableHashSet(IEnumerable<T> data) {
            _set = new HashSet<T>(data);
        }
        public ImmutableHashSet() {
            _set = new HashSet<T>();
        }

        public static ISet<T> Empty() {
            return new ImmutableHashSet<T>();
        }
        public int Count => _set.Count;

        public bool IsReadOnly => true;

        public bool Add(T item)
        {
            return false;
        }

        public void Clear()
        {
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)_set).Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)_set).CopyTo(array, arrayIndex);
        }

        public void ExceptWith(IEnumerable<T> other)
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_set).GetEnumerator();
        }

        public void IntersectWith(IEnumerable<T> other)
        {
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return ((ISet<T>)_set).IsProperSubsetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return ((ISet<T>)_set).IsProperSupersetOf(other);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return ((ISet<T>)_set).IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return ((ISet<T>)_set).IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return ((ISet<T>)_set).Overlaps(other);
        }

        public bool Remove(T item)
        {
            return false;
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return ((ISet<T>)_set).SetEquals(other);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
        }

        public void UnionWith(IEnumerable<T> other)
        {
        }

        void ICollection<T>.Add(T item)
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_set).GetEnumerator();
        }
    }

}