using System;

namespace Budgeteer.Data;

public class DAL<T> where T : class
{
    protected readonly BudgetContext _context;

    public DAL(BudgetContext context)
    {
      _context = context;
    }

    public IEnumerable<T> List()
    {
      return _context.Set<T>().ToList();
    }

    public void Add(T obj)
    {
      _context.Set<T>().Add(obj);
      _context.SaveChanges();
    }

    public void Update(T obj)
    {
      _context.Set<T>().Update(obj);
      _context.SaveChanges();
    }

    public void Delete(T obj)
    {
      _context.Set<T>().Remove(obj);
      _context.SaveChanges();
    }

    public T? RecoveryBy(Func<T, bool> condition)
    {
      return _context.Set<T>().FirstOrDefault(condition);
    }
}
