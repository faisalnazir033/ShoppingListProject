namespace ShoppingList.Domain.Entities;

public class SharedListResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid UserID { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string Permission { get; set; } = string.Empty;
}
