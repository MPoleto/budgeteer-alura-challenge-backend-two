namespace Budgeteer.API.Requests;

public record ExpenseRequestUpdate(int Id, string Description, double ExpenseValue, DateOnly Date) : ExpenseRequest(Description, ExpenseValue, Date);
