using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budgeteer.API.Responses;

public record ExpenseResponse(int Id, string Description, double ExpenseValue, DateOnly Date);
