using Microsoft.EntityFrameworkCore;
using Budgeteer.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionStrings:BudgetDB"];

builder.Services.AddDbContext<BudgetContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.Run();
