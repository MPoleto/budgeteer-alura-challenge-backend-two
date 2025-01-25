using Microsoft.EntityFrameworkCore;
using Budgeteer.Models;
using Microsoft.Extensions.Configuration;

namespace Budgeteer.Data;

public class BudgetContext : DbContext
{
    private readonly IConfiguration _configuration;

    public BudgetContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public BudgetContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    public DbSet<Income> Income { get; set; }
    public DbSet<Expense> Expenses { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        var connectionString = _configuration["ConnectionStrings:BudgetDB"];

        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}
