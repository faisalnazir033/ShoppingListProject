using ShoppingList.Application.DTOs;
using ShoppingList.Application.Interfaces;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Repositories;

namespace ShoppingList.Application.Services;

public class ShoppingListService(IShoppingListRepository repository, IUserService userService) : IShoppingListService
{
    private readonly IShoppingListRepository repository = repository;
    private readonly IUserService userService = userService;
    public async Task<bool> ShareListAsync(ShareRequest request)
    {
        var user = await userService.GetUserByEmailAsync(request.SharedWith);

        var sharedList = new SharedList
        {
            Id = request.ListId,
            UserId = user.Id,
            Permission = request.Permission,
            ShoppingListId = request.ListId,
        };

        return await repository.ShareListAsync(sharedList);
    }

    public async Task<IEnumerable<SharedListResponse>> GetSharedListsAsync(Guid userId)
    {
        var results = await repository.GetSharedListsAsync(userId);

        return results.Select(item => new SharedListResponse
        {
            Id = item.ShoppingListId,
            Name = item.ShoppingList?.Name,
            Permission = item.Permission,
            UserEmail = item.User?.Email,
            UserID = item.User.Id,
            UserName = item.User?.Name
        }).ToList();
    }
}
