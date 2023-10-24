using Microsoft.EntityFrameworkCore;

namespace project_api_poo_ii.Models;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
}