using System;
using System.Collections.Generic;
using System.Linq;

public class Agency : IAgency
{
    private readonly Dictionary<string, Invoice> _bySerialNumber;
    private HashSet<Invoice> _payed;

    public Agency()
    {
        this._bySerialNumber = new Dictionary<string, Invoice>();
        this._payed = new HashSet<Invoice>();
    }

    public bool Contains(string number)
    {
        return this._bySerialNumber.ContainsKey(number);
    }

    public int Count()
    {
        return this._bySerialNumber.Count;
    }

    public void Create(Invoice invoice)
    {
        if (this.Contains(invoice.SerialNumber))
        {
            throw new ArgumentException();
        }

        this._bySerialNumber.Add(invoice.SerialNumber, invoice);
    }

    public void PayInvoice(DateTime due)
    {
        var result = this._bySerialNumber.Values
            .Where(x => x.DueDate.Date == due.Date);

        if (!result.Any())
        {
            throw new ArgumentException();
        }

        foreach (var invoice in result)
        {
            invoice.Subtotal = 0;
            this._payed.Add(invoice);
        }
    }

    public void ThrowInvoice(string number)
    {
        if (!this.Contains(number))
        {
            throw new ArgumentException();
        }

        this._bySerialNumber.Remove(number);

    }
    public void ThrowPayed()
    {
        foreach (var invoice in this._payed)
        {
            this._bySerialNumber.Remove(invoice.SerialNumber);
        }

        this._payed = new HashSet<Invoice>();
    }

    public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
    {
        return this._bySerialNumber.Values
            .Where(x => x.IssueDate >= start && x.IssueDate <= end.Date)
            .OrderBy(x => x.IssueDate)
            .ThenBy(x => x.DueDate);
    }

    public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
    {
        var result = this._bySerialNumber.Values
            .Where(x => x.SerialNumber.Contains(serialNumber));

        if (!result.Any())
        {
            throw new ArgumentException();
        }

        return result.OrderByDescending(x => x.SerialNumber);
    }

    public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
    {
        var invoicesInPeriod = this._bySerialNumber.Values
            .Where(x => x.DueDate > start && x.DueDate < end.Date)
            .ToArray();

        if (invoicesInPeriod.Length == 0)
        {
            throw new ArgumentException();
        }

        foreach (var invoice in invoicesInPeriod)
        {
            this._bySerialNumber.Remove(invoice.SerialNumber);
        }

        return invoicesInPeriod;
    }

    public IEnumerable<Invoice> GetAllFromDepartment(Department department)
    {
        return this._bySerialNumber.Values
            .Where(x => x.Department == department)
            .OrderByDescending(x => x.Subtotal)
            .ThenBy(x => x.IssueDate);
    }

    public IEnumerable<Invoice> GetAllByCompany(string company)
    {
        return this._bySerialNumber.Values
            .Where(x => x.CompanyName == company)
            .OrderByDescending(x => x.SerialNumber);
    }

    public void ExtendDeadline(DateTime dueDate, int days)
    {
        var invoices = this._bySerialNumber.Values
            .Where(x => x.DueDate == dueDate)
            .ToArray();

        if (invoices.Length == 0)
        {
            throw new ArgumentException();
        }

        foreach (var invoice in invoices)
        {
            invoice.DueDate = invoice.DueDate.AddDays(days);
        }
    }
}
