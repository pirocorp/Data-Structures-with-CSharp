using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private readonly Dictionary<string, Person> _byEmail;
    private readonly Dictionary<string, SortedSet<Person>> _byDomain;
    private readonly Dictionary<string, SortedSet<Person>> _byNameAndTown;
    private readonly OrderedDictionary<int, SortedSet<Person>> _byAge;
    private readonly Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> _byTownAndAge;

    public PersonCollection()
    {
        this._byEmail = new Dictionary<string, Person>();
        this._byDomain = new Dictionary<string, SortedSet<Person>>();
        this._byNameAndTown = new Dictionary<string, SortedSet<Person>>();
        this._byAge = new OrderedDictionary<int, SortedSet<Person>>();
        this._byTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this._byEmail.ContainsKey(email))
        {
            return false;
        }

        var newPerson = new Person(email, name, age, town);

        this._byEmail.Add(email, newPerson);
        this.AddByDomain(email, newPerson);
        this.AddByNameAndTown(name, town, newPerson);
        this.AddByAge(age, newPerson);
        this.AddByTownAndAge(town, age, newPerson);

        return true;
    }

    public int Count => this._byEmail.Count;

    public Person FindPerson(string email)
    {
        if (!this._byEmail.ContainsKey(email))
        {
            return null;
        }

        var person = this._byEmail[email];
        return person;
    }

    public bool DeletePerson(string email)
    {
        if (!this._byEmail.ContainsKey(email))
        {
            return false;
        }

        this.RemoveByDomain(email);
        this.RemoveByNameAndTown(email);
        this.RemoveByAge(email);
        this.RemoveByTownByAge(email);
        this._byEmail.Remove(email);

        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        if (!this._byDomain.ContainsKey(emailDomain))
        {
            return Enumerable.Empty<Person>();
        }

        return this._byDomain[emailDomain];
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        var key = name + town;

        if (!this._byNameAndTown.ContainsKey(key))
        {
            return Enumerable.Empty<Person>();
        }

        return this._byNameAndTown[key];
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var range = this._byAge.Range(startAge, true, endAge, true);

        foreach (var set in range)
        {
            foreach (var person in set.Value)
            {
                yield return person;
            }
        }
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        if (!this._byTownAndAge.ContainsKey(town))
        {
            return Enumerable.Empty<Person>();
        }

        return this.ByTownAndAgeEnumerator(startAge, endAge, town);
    }

    private void AddByDomain(string email, Person newPerson)
    {
        var domain = GetDomain(email);

        if (!this._byDomain.ContainsKey(domain))
        {
            this._byDomain[domain] = new SortedSet<Person>();
        }

        this._byDomain[domain].Add(newPerson);
    }

    private void RemoveByDomain(string email)
    {
        var domain = GetDomain(email);
        var person = this.FindPerson(email);
        this._byDomain[domain].Remove(person);
    }

    private void AddByNameAndTown(string name, string town, Person newPerson)
    {
        var key = name + town;

        if (!this._byNameAndTown.ContainsKey(key))
        {
            this._byNameAndTown[key] = new SortedSet<Person>();
        }

        this._byNameAndTown[key].Add(newPerson);
    }

    private void RemoveByNameAndTown(string email)
    {
        var person = this.FindPerson(email);
        var key = person.Name + person.Town;
        this._byNameAndTown[key].Remove(person);
    }

    private void AddByAge(int age, Person newPerson)
    {
        if (!this._byAge.ContainsKey(age))
        {
            this._byAge[age] = new SortedSet<Person>();
        }

        this._byAge[age].Add(newPerson);
    }

    private void RemoveByAge(string email)
    {
        var person = this.FindPerson(email);
        this._byAge[person.Age].Remove(person);
    }

    private void AddByTownAndAge(string town, int age, Person newPerson)
    {
        if (!this._byTownAndAge.ContainsKey(town))
        {
            this._byTownAndAge[town] = new OrderedDictionary<int, SortedSet<Person>>();
        }

        if (!this._byTownAndAge[town].ContainsKey(age))
        {
            this._byTownAndAge[town][age] = new SortedSet<Person>();
        }

        this._byTownAndAge[town][age].Add(newPerson);
    }

    private void RemoveByTownByAge(string email)
    {
        var person = this.FindPerson(email);
        this._byTownAndAge[person.Town][person.Age].Remove(person);
    }

    private IEnumerable<Person> ByTownAndAgeEnumerator(int startAge, int endAge, string town)
    {
        var range = this._byTownAndAge[town].Range(startAge, true, endAge, true);

        foreach (var set in range)
        {
            foreach (var person in set.Value)
            {
                yield return person;
            }
        }
    }
    
    private static string GetDomain(string email)
    {
        var index = email.IndexOf('@') + 1;
        return email.Substring(index);
    }
}