using System;

public class Interval
{
    public decimal Lo { get; }
    public decimal Hi { get; }

    public Interval(decimal lo, decimal hi)
    {
        ValidateInterval(lo, hi);
        this.Lo = lo;
        this.Hi = hi;
    }

    public bool Intersects(decimal lo, decimal hi)
    {
        ValidateInterval(lo, hi);
        return this.Lo < hi && this.Hi > lo;
    }

    public override bool Equals(object obj)
    {
        if (obj == this) return true;
        if (obj == null) return false;
        if (obj.GetType() != this.GetType()) return false;
        var other = (Interval)obj;
        return this.Lo == other.Lo && this.Hi == other.Hi;
    }

    public override int GetHashCode()
    {
        return this.Lo.GetHashCode() ^ this.Hi.GetHashCode();
    }

    public override string ToString()
    {
        return $"({this.Lo}, {this.Hi})";
    }

    private static void ValidateInterval(decimal lo, decimal hi)
    {
        if (hi < lo)
        {
            throw new ArgumentException();
        }
    }
}