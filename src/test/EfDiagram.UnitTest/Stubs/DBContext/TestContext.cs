using Microsoft.EntityFrameworkCore;

namespace EfDiagram.UnitTest.Analyzer.DBContext
{
    public class TestContext : DbContext {

        public DbSet<TestEntity> TestEntity { get; set; }
        public DbSet<SubTestEntity> SubTestEntity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.ApplyConfiguration(new TestEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SubTestEntityConfiguration());
            modelBuilder.ApplyConfiguration(new Test2EntityConfiguration());
        }        
    }
}
