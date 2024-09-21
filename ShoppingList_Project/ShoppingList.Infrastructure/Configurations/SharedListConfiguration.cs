using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Infrastructure.Configurations;
public class SharedListConfiguration : IEntityTypeConfiguration<SharedList>
{
    public void Configure(EntityTypeBuilder<SharedList> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(u => u.Permission).IsRequired().HasMaxLength(10);
        builder.HasOne<Domain.Entities.ShoppingList>()
              .WithMany()
              .HasForeignKey(s => s.Id);
    }
}
