using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Entities;

namespace Test.Context.Configurations {
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer> {
        public void Configure(EntityTypeBuilder<Customer> builder) {
            builder.ToTable(nameof(Customer)).HasKey(p => p.Id).IsClustered();
            builder.Property(p => p.Id).HasColumnType("uniqueidentifier");
            builder.Property(p => p.LastName).HasColumnType("nvarchar(200)");
            builder.Property(p => p.FirstName).HasColumnType("nvarchar(200)");
            builder.Property(p => p.Email).HasColumnType("nvarchar(200)");
        }
    }
}
