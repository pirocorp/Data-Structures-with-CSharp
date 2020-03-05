using System;

public class Computer
{
    public Computer(int number, Brand brand, double price, double screenSize, string color)
    {
        this.Number = number;
        this.RAM = 8;
        this.Brand = brand;
        this.Price = price;
        this.ScreenSize = screenSize;
        this.Color = color;
    }
    public int Number { get; }

    public int RAM { get; set; }

    public Brand Brand { get; set; }

    public double Price { get; set; }

    public double ScreenSize { get; set; }

    public string Color { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }

        var element = (Computer)obj;
        var equals = Equals(this.Number, element.Number);
        return equals;
    }

    public override int GetHashCode()
    {
        return this.Number.GetHashCode();
    }
}
