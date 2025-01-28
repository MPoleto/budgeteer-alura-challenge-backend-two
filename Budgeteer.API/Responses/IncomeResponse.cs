using System;
namespace Budgeteer.API.Responses;

public record IncomeResponse(int Id, string Description, double IncomeValue, DateOnly Date);
