namespace Budgeteer.API.Requests;

public record IncomeRequestUpdate : IncomeRequest
{
    public int Id { get; set; }
}
