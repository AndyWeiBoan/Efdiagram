using System.Collections.Generic;
using System.Linq;
using EfDiagram.Analyzer;
using EfDiagram.Domain;
using EfDiagram.Domain.Contracts;
using EfDiagram.Domain.Pocos;
using EfDiagram.UnitTest.Analyzer.DBContext;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace EfDiagram.UnitTest.Analyzer {
    public sealed class DbContextAnalyzerTest {

        private readonly IEfdiagramAnalyzer<DbContext> _target;

        public DbContextAnalyzerTest() {
            this._target = new DbContextAnalyzer();
        }

        [Fact]
        public void ResolveTest() {
            // arrange
            var dbContext = new TestContext();
            var columns = new List<Column> {
                new Column {
                    Name = nameof(TestEntity.Id),
                    IsForeignKey = false,
                    IsPrimaryKey = true,
                    Type = "int",
                },
                new Column {
                    Name = nameof(TestEntity.Name),
                    IsForeignKey = false,
                    IsPrimaryKey = true,
                    Type = "nvarchar(50)",
                },
                new Column {
                    Name = nameof(TestEntity.Amount),
                    IsForeignKey = false,
                    IsPrimaryKey = false,
                    Type = "decimal(19,4)",
                },
                new Column {
                    Name = nameof(TestEntity.CreatedDate),
                    IsForeignKey = false,
                    IsPrimaryKey = false,
                    Type = "datetimeoffset",
                }
            };
            var expected = new EfDaigramModel {
                Entities = new List<Entity> {
                    new Entity {
                        Name = nameof(TestEntity),
                        Columns = columns.OrderByDescending(p=> p.IsPrimaryKey).ThenByDescending(p=> p.Name)
                    }
                }
            };

            // act
            var actual = this._target.Resolve(dbContext);

            // assert
            Assert.True(expected.Equals(actual));
            
        }
    }
}
