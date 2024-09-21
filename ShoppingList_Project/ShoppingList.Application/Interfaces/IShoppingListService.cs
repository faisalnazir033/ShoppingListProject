using ShoppingList.Application.DTOs;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Interfaces;
public interface IShoppingListService
{
    Task<bool> ShareListAsync(ShareRequest request);
    Task<IEnumerable<SharedListResponse>> GetSharedListsAsync(Guid userId);
}
