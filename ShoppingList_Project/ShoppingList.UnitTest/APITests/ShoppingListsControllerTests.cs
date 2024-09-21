using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingList.API.Controllers;
using ShoppingList.Application.DTOs;
using ShoppingList.Application.Interfaces;
using ShoppingList.Domain.Entities;

namespace ShoppingList.UnitTests.APITests;
public class ShoppingListControllerTests
{
    private readonly Mock<IShoppingListService> mockService;
    private readonly ShoppingListController controller;

    public ShoppingListControllerTests()
    {
        mockService = new Mock<IShoppingListService>();
        controller = new ShoppingListController(mockService.Object);
    }

    [Fact]
    public async Task ShareShoppingList_ShouldReturnOk_WhenShareIsSuccessful()
    {
        var request = new ShareRequest { ListId = "1", Permission = "read", SharedWith = "user1@example.com" };
        mockService.Setup(s => s.ShareListAsync(request)).ReturnsAsync(true);
        var result = await controller.ShareShoppingList(request);           
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Item is shared", okResult.Value);
    }

    [Fact]
    public async Task ShareShoppingList_ShouldReturnBadRequest_WhenShareFails()
    {
        var request = new ShareRequest { ListId = "123", Permission = "read", SharedWith = "user12123@example.com" };
        mockService.Setup(s => s.ShareListAsync(request)).ReturnsAsync(false);
        var result = await controller.ShareShoppingList(request);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Failed to share the shopping list.", badRequestResult.Value);
    }

    [Fact]
    public async Task GetSharedShoppingLists_ShouldReturnOk_WhenUserIdIsValid()
    {
        var userId = Guid.NewGuid().ToString();
        var expectedLists = new List<SharedListResponse>();
        mockService.Setup(s => s.GetSharedListsAsync(It.IsAny<Guid>())).ReturnsAsync(expectedLists);
        var result = await controller.GetSharedShoppingLists(userId);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualLists = Assert.IsAssignableFrom<List<SharedListResponse>>(okResult.Value);
        Assert.Equal(expectedLists, actualLists);
    }

    [Fact]
    public async Task GetSharedShoppingLists_ShouldReturnBadRequest_WhenUserIdIsInvalid()
    {
        var invalidUserId = "invalid-guid";
        var result = await controller.GetSharedShoppingLists(invalidUserId);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Invalid user ID.", badRequestResult.Value);
    }
}
