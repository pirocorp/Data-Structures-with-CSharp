namespace _04.Ordered_Set
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class OrderedSet<T> : IEnumerable<T> where T : IComparable
    {
        private readonly RedBlackTree<T> _tree;

        public OrderedSet()
        {
            this._tree = new RedBlackTree<T>(new Comparer());
        }

        public void Add(T key)
        {
            this._tree.Insert(key, DuplicatePolicy.DoNothing, out var x);
        }

        public bool Contains(T key)
        {
            return this._tree.Find(key, true, false, out var element);
        }

        public void Remove(T key)
        {
            this._tree.Delete(key, true, out var deleted);
        }

        public int Count => this._tree.ElementCount;
        
        public IEnumerator<T> GetEnumerator()
        {
            return this._tree.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Comparer : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                if (x == null || y == null)
                {
                    throw new NullReferenceException();
                }

                return x.CompareTo(y);
            }
        }
    }
}