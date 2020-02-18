namespace _02._Trie
{
    using System;

    public static class TrieProgram
    {
        private static string[] Keys = {"the", "a", "there", "answer",
            "any", "by", "bye", "their"};

        private static readonly string[] Output = { "Not present in trie", "Present in trie" };
        
        public static void Main()
        {
            InsertAndSearchTest();
            RemoveTest();
        }

        private static void RemoveTest()
        {
            Keys = new [] {
                "the", "a", "there", 
                "answer", "any", "by", 
                "bye", "their", "hero", "heroplane" };

            var trie = new Trie();

            for (var i = 0; i < Keys.Length; i++)
            {
                trie.Insert(Keys[i]);
            }

            CheckForElement(trie, "the");
            CheckForElement(trie, "these");

            CheckForElement(trie, "heroplane");
            trie.Remove("heroplane");
            Console.WriteLine("heroplane is deleted");
            CheckForElement(trie, "heroplane");

            CheckForElement(trie, "hero");

        }

        private static void InsertAndSearchTest()
        {
            var trie = new Trie();

            for (var i = 0; i < Keys.Length; i++)
            {
                trie.Insert(Keys[i]);
            }

            CheckForElement(trie, "the");
            CheckForElement(trie, "these");
            CheckForElement(trie, "their");
            CheckForElement(trie, "thaw");
        }

        private static void CheckForElement(Trie trie, string key)
        {
            if (trie.Search(key))
            {
                Console.WriteLine($"{key} --- {Output[1]}");
            }
            else
            {
                Console.WriteLine($"{key} --- {Output[0]}");
            }
        }
    }
}