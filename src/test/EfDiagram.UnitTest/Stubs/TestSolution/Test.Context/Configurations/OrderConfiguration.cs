using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Entities;

namespace Test.Context.Configurations {
    public class OrderConfiguration : IEntityTypeConfiguration<Order> {
        public void Configure(EntityTypeBuilder<Order> builder) {
            builder.ToTable(nameof(Order)).HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Id).HasColumnType("uniqueidentifier");
            builder.Property(p => p.CustomerId).HasColumnType("uniqueidentifier");
            builder.Property(p => p.Amount).HasColumnType("decimal(19,4)");
            builder.HasOne(p => p.Customer).WithMany(p => p.Orders)
                .HasForeignKey(p => p.CustomerId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
