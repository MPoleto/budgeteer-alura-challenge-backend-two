using System;

namespace Budgeteer.Models;

public class Expense
{
    public int Id { get; set; }
    public string Description { get; set; }
    public double ExpenseValue { get; set; }
    public DateOnly Date { get; set; }

    public Expense(string description, double expenseValue, DateOnly date)
    {
        Description = description;
        ExpenseValue = expenseValue;
        Date = date;
    }
}
