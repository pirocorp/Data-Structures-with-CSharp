namespace _02._Trie
{
    public class Trie
    {
        private const int ALPHABET_SIZE = 26;

        private TrieNode _root;

        public Trie()
        {
            this._root = new TrieNode();
        }

        public void Insert(string key)
        {
            key = key.ToLower();

            int level;
            var length = key.Length;

            var current = this._root;

            for (level = 0; level < length; level++)
            {
                var index = key[level] - 'a';

                if (current.Children[index] == null)
                {
                    current.Children[index] = new TrieNode();
                }

                current = current.Children[index];
            }

            //Mark last node as leaf
            current.IsEndOfWord = true;
        }

        public bool Search(string key)
        {
            key = key.ToLower();

            int level;
            var length = key.Length;

            var current = this._root;

            for (level = 0; level < length; level++)
            {
                var index = key[level] - 'a';

                if (current.Children[index] == null)
                {
                    return false;
                }

                current = current.Children[index];
            }

            return (current != null && current.IsEndOfWord);
        }

        public bool IsEmpty()
        {
            return this.IsEmpty(this._root);
        }

        public void Remove(string key)
        {
            if (!this.Search(key))
            {
                //Or throw exception for key is missing / not found
                return;
            }

            this._root = this.Remove(this._root, key);
        }

        private TrieNode Remove(TrieNode node, string key, int depth = 0)
        {
            key = key.ToLower();

            if (node == null)
            {
                return null;
            }

            if (depth == key.Length)
            {
                // This node is no more end of word after 
                // removal of given key 
                node.IsEndOfWord = false;

                //Is not prefix
                if (this.IsEmpty(node))
                {
                    node = null;
                }

                return node;
            }

            var index = key[depth] - 'a';
            node.Children[index] = this.Remove(node.Children[index], key, depth + 1);

            //If has no child and is not end of another key gets deleted
            if (this.IsEmpty(node) && node.IsEndOfWord == false)
            {
                node = null;
            }

            return node;
        }

        private bool IsEmpty(TrieNode node)
        {
            for (var i = 0; i < ALPHABET_SIZE; i++)
            {
                if (node.Children[i] != null)
                {
                    return false;
                }
            }

            return true;
        }

        private class TrieNode
        {
            public TrieNode[] Children { get; set; }

            public bool IsEndOfWord { get; set; }

            public TrieNode()
            {
                this.IsEndOfWord = false;
                this.Children = new TrieNode[ALPHABET_SIZE];

                for (var i = 0; i < ALPHABET_SIZE; i++)
                {
                    Children[i] = null;
                }
            }
        }
    }
}