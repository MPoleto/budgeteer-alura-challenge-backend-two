using System.ComponentModel.DataAnnotations;


namespace Budgeteer.API.Requests;

public record IncomeRequest([Required] string Description, [Required] double IncomeValue, [Required] DateOnly Date);
