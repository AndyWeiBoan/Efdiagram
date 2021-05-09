using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EfDiagram.UnitTest.Analyzer.DBContext {
    public class TestEntityConfiguration : IEntityTypeConfiguration<TestEntity> {
        public void Configure(EntityTypeBuilder<TestEntity> builder) {
            builder.ToTable(nameof(TestEntity)).HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Id).HasColumnType("int");
            builder.Property(p => p.Name).HasColumnType("nvarchar(50)");
            builder.Property(p => p.Amount).HasColumnType("decimal(19,4)");
            builder.Property(p => p.CreatedDate).HasColumnType("datetimeoffset");
            builder.HasMany(p => p.SubTests).WithOne(p => p.Test).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
