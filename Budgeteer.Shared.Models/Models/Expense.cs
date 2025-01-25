using System;

namespace Budgeteer.Models;

public class Expense
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public double ExpenseValue { get; set; } = 0;
    public DateOnly Date { get; set; } = new DateOnly();
}
