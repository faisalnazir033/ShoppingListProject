using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Data;

namespace ShoppingList.Infrastructure.Repositories;
public interface IShoppingListRepository
{
    Task<bool> ShareListAsync(SharedList sharedList);
    Task<IEnumerable<SharedList>> GetSharedListsAsync(Guid userId);
}

public class ShoppingListRepository(ApplicationDbContext context) : IShoppingListRepository
{
    public async Task<bool> ShareListAsync(SharedList sharedList)
    {
        context.SharedLists.Add(sharedList);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<SharedList>> GetSharedListsAsync(Guid userId)
    {
        return await context.SharedLists.Include(x => x.User).Include(x => x.ShoppingList)
       .Where(s => s.UserId == userId)
       .ToListAsync();
    }
}