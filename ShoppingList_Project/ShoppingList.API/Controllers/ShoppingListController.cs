using Microsoft.AspNetCore.Mvc;
using ShoppingList.API.Filters;
using ShoppingList.Application.DTOs;
using ShoppingList.Application.Interfaces;

namespace ShoppingList.API.Controllers;

[ServiceFilter(typeof(TokenValidationFilter))]
[Route("api/[controller]")]
[ApiController]
public class ShoppingListController(IShoppingListService shoppingListService) : ControllerBase
{
    private readonly IShoppingListService _shoppingListService = shoppingListService;

    [HttpPost("share")]
    public async Task<IActionResult> ShareShoppingList([FromBody] ShareRequest request)
    {
        var result = await _shoppingListService.ShareListAsync(request);
        return result ? Ok("Item is shared") : BadRequest("Failed to share the shopping list.");
    }

    [HttpGet("shared/{userId}")]
    public async Task<IActionResult> GetSharedShoppingLists(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId) || !Guid.TryParse(userId, out var validUserId))        
            return BadRequest("Invalid user ID.");
        
        var lists = await _shoppingListService.GetSharedListsAsync(validUserId);

        return Ok(lists);
    }
}
