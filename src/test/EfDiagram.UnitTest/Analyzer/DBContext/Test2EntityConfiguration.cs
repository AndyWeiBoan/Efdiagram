using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfDiagram.UnitTest.Analyzer.DBContext {
    public sealed class Test2EntityConfiguration : IEntityTypeConfiguration<Test2Entity> {
        public void Configure(EntityTypeBuilder<Test2Entity> builder) {
            builder.ToTable(nameof(Test2Entity)).HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Id).HasColumnType("int");
            builder.Property(p => p.Name).HasColumnType("nvarchar(50)");
            builder.Property(p => p.Amount).HasColumnType("decimal(19,4)");
            builder.Property(p => p.CreatedDate).HasColumnType("datetimeoffset");
            builder.HasMany(p => p.SubTests).WithOne(p => p.Test2).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
