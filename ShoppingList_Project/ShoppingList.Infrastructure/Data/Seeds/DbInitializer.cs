using ShoppingList.Domain.Entities;

namespace ShoppingList.Infrastructure.Data.Seeds;
public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();
        if (!context.Users.Any())
        {
            var user1 = new User { Id = new Guid("123e4567-e89b-12d3-a456-426614174000"), Name = "user1", Email = "user1@example.com", PasswordHash = "P@ssword1" };
            var user2 = new User { Id = new Guid("223e4589-e89b-12d3-a456-426614174000"), Name = "user2", Email = "user2@example.com", PasswordHash = "P@ssword2" };

            context.Users.AddRange(user1, user2);
            context.SaveChanges();
        }

        if (!context.ShoppingLists.Any())
        {
            var user1 = context.Users.First(u => u.Id == new Guid("123e4567-e89b-12d3-a456-426614174000"));
            var user2 = context.Users.First(u => u.Id == new Guid("223e4589-e89b-12d3-a456-426614174000"));

            var shoppingList1 = new Domain.Entities.ShoppingList { Id = "1", UserId = user1.Id, Name = "Groceries" };
            var shoppingList2 = new Domain.Entities.ShoppingList { Id = "2", UserId = user1.Id, Name = "Hardware" };
            var shoppingList3 = new Domain.Entities.ShoppingList { Id = "3", UserId = user2.Id, Name = "Clothing" };

            context.ShoppingLists.AddRange(shoppingList1, shoppingList2, shoppingList3);
            context.SaveChanges();
        }

        if (!context.SharedLists.Any())
        {
            var shoppingList1 = context.ShoppingLists.First(s => s.Id == "1");
            var shoppingList3 = context.ShoppingLists.First(s => s.Id == "3");

            var sharedList1 = new SharedList { Id = "1", UserId = new Guid("123e4567-e89b-12d3-a456-426614174000"), Permission = "read", ShoppingList = shoppingList1 };
            var sharedList2 = new SharedList { Id = "2", UserId = new Guid("223e4589-e89b-12d3-a456-426614174000"), Permission = "write", ShoppingList = shoppingList3 };

            context.SharedLists.AddRange(sharedList1, sharedList2);
            context.SaveChanges();
        }
    }
}
