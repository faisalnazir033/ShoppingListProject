namespace ShoppingList.Application.DTOs;

public class ShareRequest
{
    public string ListId { get; set; } = string.Empty;
    public string SharedWith { get; set; } = string.Empty;
    public string Permission { get; set; } = string.Empty;
}
