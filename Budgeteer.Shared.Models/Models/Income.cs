using System;

namespace Budgeteer.Models;

public class Income
{
    public int Id { get; set; }
    public string Description { get; set; }
    public double IncomeValue { get; set; }
    public DateOnly Date { get; set; }

    public Income(string description, double incomeValue, DateOnly date)
    {
        Description = description;
        IncomeValue = incomeValue;
        Date = date;
    }
}
