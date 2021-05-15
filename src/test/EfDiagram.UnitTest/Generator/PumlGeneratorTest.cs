using System.Collections.Generic;
using EfDiagram.Domain;
using EfDiagram.Domain.Contracts;
using EfDiagram.Domain.Pocos;
using EfDiagram.Parsers.PlantUml;
using Xunit;

namespace EfDiagram.UnitTest.Generator {

    public sealed class PumlGeneratorTest {

        private readonly IDigramGenerator _target;

        public PumlGeneratorTest() {
            this._target = new PumlGenerator();
        }

        [Fact]
        public void GetResultTest() {
            // arrange
            var model = new EfDaigramModel {
                Entities = new List<Entity> {
                    new Entity {
                        Name = "e01", 
                        Columns = new List<Column> { 
                            new Column { 
                                Name = "id", Type = "int"
                            },
                            new Column {
                                Name = "name", Type = "int"
                            }
                        },

                    },
                    new Entity {
                        Name = "e02",
                        Columns = new List<Column> {
                            new Column {
                                Name = "id", Type = "int"
                            }
                        }
                    }
                },
            };

            // act
            var result = this._target.GetResult(model);

            Assert.Contains("@startum", result);
            Assert.Contains("@enduml", result);
        }
    }
}
