using System.Collections.Generic;
using System.Linq;
using EfDiagram.Analyzer;
using EfDiagram.Domain;
using EfDiagram.Domain.Contracts;
using EfDiagram.Domain.Pocos;
using EfDiagram.Parsers.PlantUml;
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

            var relaionShips = new List<TableRelationShip> { 
                new TableRelationShip {
                    Entity = new Entity {Name = nameof(SubTestEntity)},
                    Principal = new Entity {Name = nameof(TestEntity)},
                    Type = RelationShipType.OneToMany
                },
                new TableRelationShip {
                    Entity = new Entity {Name = nameof(SubTestEntity)},
                    Principal = new Entity {Name = nameof(Test2Entity)},
                    Type = RelationShipType.OneToMany
                },
            };

            // act
            var actual = this._target.Resolve(dbContext);
            IEfDigramParser p = new PlantUmlParser();
            var a = p.GetResult(actual);
            // assert
            Assert.Contains(actual.RelationShips, p => p.Principal.Name == nameof(TestEntity));
            Assert.Contains(actual.RelationShips, p => p.Principal.Name == nameof(Test2Entity));
            Assert.Contains(actual.RelationShips, p => p.Type == RelationShipType.OneToMany);
        }
    }
}
