using Microsoft.EntityFrameworkCore;
using Budgeteer.Data;
using Budgeteer.Models;
using Budgeteer.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionStrings:BudgetDB"];

builder.Services.AddDbContext<BudgetContext>(options => 
  options.UseMySql(connectionString, 
    ServerVersion.AutoDetect(connectionString), 
    x => x.MigrationsAssembly("Budgeteer.Shared.Data")));

builder.Services.AddTransient<DAL<Income>>();
builder.Services.AddTransient<DAL<Expense>>();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.RegisterIncomeEndpoints();
app.RegisterExpenseEndpoints();

app.Run();
