using System;
using Microsoft.AspNetCore.Mvc;
using Budgeteer.Data;
using Budgeteer.Models;
using Budgeteer.API.Requests;
using Budgeteer.API.Responses;

namespace Budgeteer.API.Endpoints;

public static class IncomeEndpoints
{
    public static void RegisterIncomeEndpoints(this WebApplication app)
    {
        var incomeGroup = app.MapGroup("/receitas");

        incomeGroup.MapPost("/", ([FromServices] DAL<Income> dal, [FromBody] IncomeRequest request) => 
        {         
            var verifyIncome = dal.RecoverBy(i => i.Date.Month == request.Date.Month &&
                    i.Date.Year == request.Date.Year &&
                    i.Description.ToUpper().Equals(request.Description.ToUpper()));
          
            if (verifyIncome is null)
            {
                var income = new Income(request.Description, request.IncomeValue, request.Date);
                dal.Add(income);
                return Results.Ok(income);
            }
            if (verifyIncome is not null)
            {
                return Results.BadRequest("Verifique se esta receita já foi adicionada."); 
            }
            return Results.BadRequest("Algo deu errado");
        });

        incomeGroup.MapGet("/", ([FromServices] DAL<Income> dal) => 
        {
            var responseList = ResponseList(dal.List());
            return Results.Ok(responseList);
        });

        incomeGroup.MapGet("/{id}", ([FromServices] DAL<Income> dal, int id) => 
        {
            var income = dal.RecoverBy(i => i.Id == id);

            if (income is null) return Results.NotFound();

            var IncomeResponse = EntityToResponse(income);
            return Results.Ok(IncomeResponse);
        });

        incomeGroup.MapPut("/{id}", ([FromServices] DAL<Income> dal, [FromBody] IncomeRequestUpdate request, int id) =>
        {
            var incomeUpdate = dal.RecoverBy(i => i.Id == id);

            if (incomeUpdate is null) return Results.NotFound();

            var verifyIncome = dal.RecoverBy(i => i.Id != id &&
                i.Date.Month == request.Date.Month &&
                i.Date.Year == request.Date.Year &&
                i.Description.ToUpper().Equals(request.Description.ToUpper()));
            
            if (verifyIncome is not null)
            {
                return Results.BadRequest("Verifique se esta receita já foi adicionada."); 
            }

            incomeUpdate.Description = request.Description;
            incomeUpdate.IncomeValue = request.IncomeValue;
            incomeUpdate.Date = request.Date;

            dal.Update(incomeUpdate);
            return Results.Ok(incomeUpdate);
        });

        incomeGroup.MapDelete("/{id}", ([FromServices] DAL<Income> dal, int id) =>
        {
            var income = dal.RecoverBy(i => i.Id == id);
            if (income is null) return Results.NotFound();
            dal.Delete(income);
            return Results.NoContent();
        });
    }

    private static ICollection<IncomeResponse> ResponseList(IEnumerable<Income> incomeList)
    {
        return incomeList.Select(EntityToResponse).ToList();
    }

    private static IncomeResponse EntityToResponse(Income income)
    {
        return new IncomeResponse(income.Id, income.Description, income.IncomeValue, income.Date);
    }
}
