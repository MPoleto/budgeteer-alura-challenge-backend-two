using System;
using System.ComponentModel.DataAnnotations;

namespace Budgeteer.API.Requests;

public record ExpenseRequest
{
    public required string Description { get; set; }
    public required double ExpenseValue { get; set; }
    public required DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}
