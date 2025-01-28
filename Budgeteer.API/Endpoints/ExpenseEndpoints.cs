using System;
using Microsoft.AspNetCore.Mvc;
using Budgeteer.Data;
using Budgeteer.Models;
using Budgeteer.API.Requests;
using Budgeteer.API.Responses;

namespace Budgeteer.API.Endpoints;

public static class ExpenseEndpoints
{
    public static void RegisterExpenseEndpoints(this WebApplication app)
    {
        var expenseGroup = app.MapGroup("/despesas");

        expenseGroup.MapPost("/", ([FromServices] DAL<Expense> dal, [FromBody] ExpenseRequest request) => 
        {
            var verifyExpense = dal.RecoverBy(e => 
                      e.Date.Month == request.Date.Month && 
                      e.Date.Year == request.Date.Year &&
                      e.Description.ToUpper().Equals(request.Description.ToUpper()));
            
            if (verifyExpense is not null)
            {
              return Results.BadRequest("Verifique se esta despesa já foi adicionada");
            }
            if (verifyExpense is null)
            {
              var expense = new Expense(request.Description, request.ExpenseValue, request.Date);
              dal.Add(expense);
              return Results.Ok(expense);
            }
            return Results.BadRequest("Algo deu errado");
        });

        expenseGroup.MapGet("/", ([FromServices] DAL<Expense> dal) =>
        {
          var responseList = ResponseList(dal.List());
          return Results.Ok(responseList);
        });

        expenseGroup.MapGet("/{id}", ([FromServices] DAL<Expense> dal, int id) =>
        {
          var expense = dal.RecoverBy(e => e.Id == id);

          if (expense is null) return Results.NotFound();

          var expenseResponse = EntityToResponse(expense);
          return Results.Ok(expenseResponse);
        });

        expenseGroup.MapPut("/{id}", ([FromServices] DAL<Expense> dal, [FromBody] ExpenseRequestUpdate request, int id) =>
        {
            var expenseUpdate = dal.RecoverBy(e => e.Id == id);

            if (expenseUpdate is null) return Results.NotFound();

            var verifyExpense = dal.RecoverBy(e => e.Id != id &&
                      e.Date.Month == request.Date.Month && 
                      e.Date.Year == request.Date.Year &&
                      e.Description.ToUpper().Equals(request.Description.ToUpper()));
            
            if (verifyExpense is not null)
            {
              return Results.BadRequest("Verifique se esta despesa já foi adicionada");
            }

            expenseUpdate.Description = request.Description;
            expenseUpdate.ExpenseValue = request.ExpenseValue;
            expenseUpdate.Date = request.Date;

            dal.Update(expenseUpdate);
            return Results.Ok(expenseUpdate);
        });

        expenseGroup.MapDelete("/{id}", ([FromServices] DAL<Expense> dal, int id) => 
        {
          var expense = dal.RecoverBy(e => e.Id == id);
          if (expense is null) return Results.NotFound();
          dal.Delete(expense);
          return Results.NoContent();
        });
    }

    private static ICollection<ExpenseResponse> ResponseList(IEnumerable<Expense> expenseList)
    {
      return expenseList.Select(EntityToResponse).ToList();
    }

    private static ExpenseResponse EntityToResponse(Expense expense)
    {
      return new ExpenseResponse(expense.Id, expense.Description, expense.ExpenseValue, expense.Date);
    }
}
