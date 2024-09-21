using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Configurations;
using ShoppingList.Infrastructure.Data.Seeds;

namespace ShoppingList.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Domain.Entities.ShoppingList> ShoppingLists { get; set; }
    public DbSet<SharedList> SharedLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ShoppingListConfiguration());
        modelBuilder.ApplyConfiguration(new SharedListConfiguration());

        modelBuilder.Entity<Domain.Entities.ShoppingList>()
            .HasMany(sl => sl.SharedList)
            .WithOne(s => s.ShoppingList)
            .HasForeignKey(s => s.ShoppingListId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.ShoppingLists)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
    public void EnsureSeedData()
    {
        DbInitializer.Seed(this);
    }
}
