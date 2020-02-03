using System;
using System.Collections;
using System.Collections.Generic;

namespace _01._Play_with_Trees
{
    public class Tree<T>
    {
        public T Value { get; set; }

        public Tree<T> Parent { get; set; }

        public IList<Tree<T>> Children { get; private set; }

        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>(children);

            foreach (var child in this.Children)
            {
                child.Parent = this;
            }
        }

        public void Each()
        {
            this.Each(this);
        }

        public override string ToString()
        {
            return $"Value: {this.Value}";
        }

        private void Each(Tree<T> currentTree)
        {
            if (currentTree.Children.Count > 0)
            {
                for (var i = 0; i < currentTree.Children.Count; i++)
                {
                    currentTree.Each(currentTree.Children[i]);
                }
            }

            Console.WriteLine(currentTree.Value);
        }
    }
}
