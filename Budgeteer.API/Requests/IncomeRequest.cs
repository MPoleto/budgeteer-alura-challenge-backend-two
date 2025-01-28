using System.ComponentModel.DataAnnotations;


namespace Budgeteer.API.Requests;

public record IncomeRequest
{
    public required string Description { get; set; }
    public required double IncomeValue { get; set; }
    public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}
