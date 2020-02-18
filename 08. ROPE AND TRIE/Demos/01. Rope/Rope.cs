namespace _01._Rope
{
    using System;

    public class Rope
    {
        private RopeNode _root;

        public Rope()
        {
            this._root = new RopeNode("");
        }

        public void Clear()
        {
            this._root = new RopeNode("");
        }

        public void Concat(string str)
        {
            var newRope = new RopeNode(str);
            var newRoot = new RopeNode("");

            newRoot.Left = this._root;
            newRoot.Right = newRope;

            newRoot.Weight = newRoot.Left.Weight;

            if (newRoot.Left.Right != null)
            {
                newRoot.Weight += newRoot.Left.Right.Weight;
            }

            this._root = newRoot;
        }

        public char CharacterAt(int index)
        {
            var current = this._root;

            if (index > current.Weight)
            {
                index -= current.Weight;
                return current.Right.Data[index];
            }

            while (index < current.Weight)
            {
                current = current.Left;
            }

            index -= current.Weight;
            return current.Right.Data[index];
        }

        public string Substring(int start, int end)
        {
            var result = "";
            var found = false;
            var current = this._root;

            if (end > current.Weight)
            {
                found = true;
                end -= current.Weight;

                if (start > current.Weight)
                {
                    start -= current.Weight;
                    result = current.Right.Data.Substring(start, end) + result;
                    return result;
                }

                result = current.Right.Data.Substring(0, end);
            }

            current = current.Left;

            while (start < current.Weight)
            {
                result = current.Right.Data + result;
                current = current.Left;
            }

            start -= current.Weight;
            result = current.Right.Data.Substring(start) + result;

            return result;
        }

        public void Print()
        {
            this.Print(this._root);
            Console.WriteLine();
        }

        private void Print(RopeNode node)
        {
            if (node != null)
            {
                this.Print(node.Left);

                if (node.Data != null)
                {
                    Console.Write(node.Data);
                }

                this.Print(node.Right);
            }
        }

        private class RopeNode
        {
            public RopeNode Left { get; set; }

            public RopeNode Right { get; set; }

            public string Data { get; private set; }

            public int Weight { get; set; }

            public RopeNode(string data)
            {
                this.Data = data;
                this.Weight = data.Length;
                this.Left = null;
                this.Right = null;
            }
        }
    }
}