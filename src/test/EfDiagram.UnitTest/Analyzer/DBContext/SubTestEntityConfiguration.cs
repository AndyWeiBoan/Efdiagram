using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDiagram.UnitTest.Analyzer.DBContext
{
    public sealed class SubTestEntityConfiguration : IEntityTypeConfiguration<SubTestEntity>
    {
        public void Configure(EntityTypeBuilder<SubTestEntity> builder)
        {
            builder.ToTable(nameof(SubTestEntity)).HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Id).HasColumnType("int");
            builder.Property(p => p.Name).HasColumnType("nvarchar(50)");
            builder.Property(p => p.Amount).HasColumnType("decimal(19,4)");
            builder.Property(p => p.CreatedDate).HasColumnType("datetimeoffset");
            builder.HasOne(p => p.Test).WithMany(p => p.SubTests).HasForeignKey(p=> p.TestId);
        }
    }
}
