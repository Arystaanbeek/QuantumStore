using Microsoft.EntityFrameworkCore;
using QuantumStore.Models;

namespace QuantumStore.Models;


public class StoreContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public StoreContext(DbContextOptions<StoreContext> options) : base(options) 
    {
    
    }
}