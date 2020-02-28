using System;

public class Person : IComparable<Person>
{
    public string Email { get; private set; }

    public string Name { get; private set; }

    public int Age { get; private set; }

    public string Town { get; private set; }

    public Person(string email, string name, int age, string town)
    {
        this.Email = email;
        this.Name = name;
        this.Age = age;
        this.Town = town;
    }

    public int CompareTo(Person other)
    {
        return string.Compare(this.Email, other.Email, StringComparison.InvariantCulture);
    }
}