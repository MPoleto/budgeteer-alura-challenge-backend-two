namespace Budgeteer.API.Requests;

public record IncomeRequestUpdate(int Id, string Description, double IncomeValue, DateOnly Date) : IncomeRequest(Description, IncomeValue, Date);
