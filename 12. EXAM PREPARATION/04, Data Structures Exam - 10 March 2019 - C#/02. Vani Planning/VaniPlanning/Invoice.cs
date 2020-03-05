using System;

public class Invoice
{
    public Invoice(string number, string company, double subtotal, Department dep, DateTime issueDate, DateTime dueDate)
    {
        this.SerialNumber = number;
        this.CompanyName = company;
        this.Subtotal = subtotal;
        this.Department = dep;
        this.IssueDate = issueDate;
        this.DueDate = dueDate;
    }
    public string SerialNumber { get; }

    public string CompanyName { get; set; }

    public double Subtotal { get; set; }

    public Department Department { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime DueDate { get; set; }

    public override string ToString()
    {
        return $"SN: {this.SerialNumber}, Issue Date: {this.IssueDate.ToShortDateString()}";
    }

    public override bool Equals(object obj)
    {
        return this.SerialNumber == ((Invoice) obj)?.SerialNumber;
    }

    public override int GetHashCode()
    {
        return this.SerialNumber.GetHashCode();
    }
}
