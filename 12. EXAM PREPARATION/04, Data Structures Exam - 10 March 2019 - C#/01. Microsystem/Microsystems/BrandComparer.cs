using System.Collections.Generic;

public class BrandComparer : IComparer<Computer>
{
    public int Compare(Computer x, Computer y)
    {
        var cmp = -x.Price.CompareTo(y.Price);

        return cmp;
    }
}