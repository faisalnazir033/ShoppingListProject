namespace ShoppingList.Domain.Entities;

public class SharedList
{
    public string Id { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string Permission { get; set; } = string.Empty;
    public string ShoppingListId { get; set; }
    public ShoppingList ShoppingList { get; set; }
    public User User { get; set; }
}
