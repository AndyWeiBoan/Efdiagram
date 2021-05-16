using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Entities;

namespace Test.Context.Configurations {
    public class ProductConfiguration : IEntityTypeConfiguration<Product> {
        public void Configure(EntityTypeBuilder<Product> builder) {
            builder.ToTable(nameof(Product)).HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Id).HasColumnType("uniqueidentifier");
            builder.Property(p => p.Name).HasColumnType("nvarchar(200)");
            builder.Property(p => p.Price).HasColumnType("decimal(19,4)");
        }
    }
}
