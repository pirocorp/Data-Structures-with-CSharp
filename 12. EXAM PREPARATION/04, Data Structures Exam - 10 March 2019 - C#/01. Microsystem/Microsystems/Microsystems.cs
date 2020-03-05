using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Microsystems : IMicrosystem
{
    private readonly Dictionary<int, Computer> _byNumber;

    public Microsystems()
    {
        this._byNumber = new Dictionary<int, Computer>();
    }

    public void CreateComputer(Computer computer)
    {
        if (this._byNumber.ContainsKey(computer.Number))
        {
            throw new ArgumentException();
        }

        this._byNumber.Add(computer.Number, computer);
    }

    public bool Contains(int number)
    {
        return this._byNumber.ContainsKey(number);
    }

    public int Count()
    {
        return this._byNumber.Count;
    }

    public Computer GetComputer(int number)
    {
        if (!this.Contains(number))
        {
            throw new ArgumentException();
        }

        return this._byNumber[number];
    }

    public void Remove(int number)
    {
        if (!this.Contains(number))
        {
            throw new ArgumentException();
        }

        this._byNumber.Remove(number);
    }

    public void RemoveWithBrand(Brand brand)
    {
        if (this._byNumber.Values.All(x => x.Brand != brand))
        {
            throw new ArgumentException();
        }

        var computersToRemove = this._byNumber.Values
            .Where(x => x.Brand == brand)
            .ToList();

        for (var i = 0; i < computersToRemove.Count(); i++)
        {
            this._byNumber.Remove(computersToRemove[i].Number);
        }
    }

    public void UpgradeRam(int ram, int number)
    {
        if (!this.Contains(number))
        {
            throw new ArgumentException();
        }

        var computerToUpgrade = this._byNumber[number];
        if (computerToUpgrade.RAM < ram)
        {
            computerToUpgrade.RAM = ram;
        }
    }

    public IEnumerable<Computer> GetAllFromBrand(Brand brand)
    {
        if (this._byNumber.Values.All(x => x.Brand != brand))
        {
            return Enumerable.Empty<Computer>();
        }

        return this._byNumber.Values
            .Where(x => x.Brand == brand)
            .OrderByDescending(x => x.Price);
    }

    public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
    {
        var result = this._byNumber.Values
            .Where(x => x.ScreenSize == screenSize)
            .OrderByDescending(x => x.Number);

        if (!result.Any())
        {
            return Enumerable.Empty<Computer>();
        }

        return result;
    }

    public IEnumerable<Computer> GetAllWithColor(string color)
    {
        if (this._byNumber.Values.All(x => x.Color != color))
        {
            return Enumerable.Empty<Computer>();
        }

        return this._byNumber.Values
            .Where(x => x.Color == color)
            .OrderByDescending(x => x.Price);
    }

    public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
    {
        var result = this._byNumber.Values
            .Where(x => x.Price >= minPrice && x.Price <= maxPrice)
            .OrderByDescending(p => p.Price);

        if (!result.Any())
        {
            return Enumerable.Empty<Computer>();
        }

        return result;
    }
}
