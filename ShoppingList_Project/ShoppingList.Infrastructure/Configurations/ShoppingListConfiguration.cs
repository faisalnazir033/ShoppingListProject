using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ShoppingList.Infrastructure.Configurations;

public class ShoppingListConfiguration : IEntityTypeConfiguration<Domain.Entities.ShoppingList>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.ShoppingList> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(20);
    }
}
