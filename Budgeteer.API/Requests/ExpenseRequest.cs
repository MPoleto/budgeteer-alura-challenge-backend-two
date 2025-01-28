using System;
using System.ComponentModel.DataAnnotations;

namespace Budgeteer.API.Requests;

public record ExpenseRequest([Required] string Description, [Required] double ExpenseValue, [Required] DateOnly Date);
