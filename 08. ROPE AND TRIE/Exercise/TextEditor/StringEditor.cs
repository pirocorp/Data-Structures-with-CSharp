using System.Collections.Generic;
using Wintellect.PowerCollections;

public class StringEditor : ITextEditor
{
    //Wintellect's BigList is Rope
    private readonly Trie<BigList<char>> usersString;
    private readonly Trie<Stack<string>> usersHistory;

    public StringEditor()
    {
        this.usersString = new Trie<BigList<char>>();
        this.usersHistory = new Trie<Stack<string>>();
    }

    public void Login(string username)
    {
        if (this.usersString.Contains(username))
        {
            return;
        }

        this.usersString.Insert(username, new BigList<char>());
        this.usersHistory.Insert(username, new Stack<string>());
    }

    public void Logout(string username)
    {
        this.usersString.Delete(username);
        this.usersHistory.Delete(username);
    }

    public string Print(string username)
    {
        if (!this.usersString.Contains(username))
        {
            return string.Empty;
        }

        return string.Join("", this.usersString.GetValue(username));
    }

    public void Prepend(string username, string str)
    {
        if (!this.usersString.Contains(username))
        {
            return; 
        }

        this.SetUndoPoint(username);

        this.usersString.GetValue(username).AddRangeToFront(str.ToCharArray());
    }
    
    public void Clear(string username)
    {
        if (!this.usersString.Contains(username))
        {
            return;
        }

        this.SetUndoPoint(username);
        this.usersString.Insert(username, new BigList<char>());
    }

    public void Delete(string username, int startIndex, int length)
    {
        if (!this.usersString.Contains(username))
        {
            return;
        }

        this.SetUndoPoint(username);

        var currentBigList = this.usersString.GetValue(username);
        currentBigList.RemoveRange(startIndex, length);
    }

    public void Insert(string username, int index, string str)
    {
        if (!this.usersString.Contains(username))
        {
            return;
        }

        this.SetUndoPoint(username);

        var currentBigList = this.usersString.GetValue(username);
        currentBigList.InsertRange(index, str);
    }

    public int Length(string username)
    {
        if (!this.usersString.Contains(username))
        {
            return 0;
        }

        return this.usersString.GetValue(username).Count;
    }

    public void Substring(string username, int startIndex, int length)
    {
        if (!this.usersString.Contains(username))
        {
            return;
        }

        this.SetUndoPoint(username);

        var currentBigList = this.usersString.GetValue(username);

        var subBigList = currentBigList.GetRange(startIndex, length);
        this.usersString.Insert(username, subBigList);
    }

    public void Undo(string username)
    {
        if (!this.usersString.Contains(username))
        {
            return;
        }

        var undoString = this.usersHistory.GetValue(username).Pop();

        var newBigList = new BigList<char>();
        newBigList.AddRange(undoString);

        this.usersString.Insert(username, newBigList);
    }

    public IEnumerable<string> Users(string prefix = "")
    {
        return this.usersString.GetByPrefix(prefix);
    }

    private void SetUndoPoint(string username)
    {
        this.usersHistory.GetValue(username)
            .Push(string.Join("", this.usersString.GetValue(username)));
    }
}