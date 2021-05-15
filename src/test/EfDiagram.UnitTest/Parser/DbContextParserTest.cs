using System.Collections.Generic;
using EfDiagram.Domain;
using EfDiagram.Domain.Contracts;
using EfDiagram.Domain.Pocos;
using EfDiagram.Parsers;
using EfDiagram.UnitTest.Analyzer.DBContext;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace EfDiagram.UnitTest.Parser {
    public sealed class DbContextParserTest {

        private readonly IEfdiagramModelParser<DbContext> _target;

        public DbContextParserTest() {
            this._target = new DbContextParser();
        }

        [Fact]
        public void ParserTest() {
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
            var actual = this._target.GetResult(dbContext);

            // assert
            Assert.Contains(actual.RelationShips, p => p.Principal.Name == nameof(TestEntity));
            Assert.Contains(actual.RelationShips, p => p.Principal.Name == nameof(Test2Entity));
            Assert.Contains(actual.RelationShips, p => p.Type == RelationShipType.OneToMany);
        }
    }
}
