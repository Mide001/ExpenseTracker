using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;


namespace ExpenseTracker.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    public DbSet<Expense> Expenses => Set<Expense>();
}