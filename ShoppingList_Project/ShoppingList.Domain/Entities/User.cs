﻿namespace ShoppingList.Domain.Entities;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } =  string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public ICollection<ShoppingList> ShoppingLists { get; set; } = [];
}
