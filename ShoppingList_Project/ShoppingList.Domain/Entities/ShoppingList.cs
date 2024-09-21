using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShoppingList.Domain.Entities;

public class ShoppingList
{
    public string Id { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;    
    public User User { get; set; }
    public ICollection<SharedList> SharedList { get; set; } = [];
}
