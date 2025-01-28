namespace Budgeteer.API.Requests;

public record ExpenseRequestUpdate : ExpenseRequest
{
    public int Id { get; set; }
}
