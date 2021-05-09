using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EfDiagram.Domain;
using EfDiagram.Domain.Contracts;
using EfDiagram.Domain.Pocos;

namespace EfDiagram.Parsers.PlantUml {
    public sealed class PlantUmlParser : IEfDigramParser {

        string IEfDigramParser.GetResult(EfDaigramModel model) {
            var result = new PlantUmlModel { Entities = new  List<string>() };
            foreach (var e in model.Entities) {
                var columns = this.GetColumns(e.Columns);
                var entity = this.GetEntity(e.Name, columns);
                result.Entities.Add(entity);
            }
            result.RelationShips = this.GetRelationShips(model.RelationShips);
            return result.ToString();
        }

        private string GetEntity(string name, string columns) {
            return $@"
entity ""{name}"" as {name} {{
{columns}
}}";
        }

        private string GetColumns(IEnumerable<Column> columns) {
            var sb = new StringBuilder();
            if (columns.Any(p=> p.IsPrimaryKey)) {                
                sb.AppendLine($"{string.Join("\r\n", columns.Where(p=> p.IsPrimaryKey).Select(p=> new string($"    {p.Name}: {p.Type}")))}");
                sb.Append($"---\r\n"); 
            }
            
            foreach (var column in columns.Where(p => !p.IsPrimaryKey)) {
                sb.AppendLine($"    {column.Name}: {column.Type} {( column.IsForeignKey ? "<<FK>>" : "" )}");
            }
            return sb.ToString();
        }

        private string GetRelationShips(IEnumerable<TableRelationShip> relations) {
            var sb = new StringBuilder();
            foreach (var relation in relations) {
                var principalSymbol = string.Empty;
                var relationSymbol = relation.Identifying ? "--" : "..";
                var symbol = string.Empty;
                if (relation.Identifying) {
                    principalSymbol = 
                        relation.Type == RelationShipType.OneToOne || relation.Type == RelationShipType.OneToMany
                        ? "||" 
                        : "}|";

                    if (relation.Type == RelationShipType.OneToOne) {
                        principalSymbol = "||";
                        symbol = "||";
                    }

                    if (relation.Type == RelationShipType.OneToMany) {
                        principalSymbol = "||";
                        symbol = "|{";
                    }

                    if (relation.Type == RelationShipType.ManyToMany) {
                        principalSymbol = "}|";
                        symbol = "|{";
                    }
                }
                else {
                    if (relation.Type == RelationShipType.OneToOne) {
                        principalSymbol = "||";
                        symbol = "o|";
                    }

                    if (relation.Type == RelationShipType.OneToMany) {
                        principalSymbol = "||";
                        symbol = "o{";
                    }

                    if (relation.Type == RelationShipType.ManyToMany) {
                        principalSymbol = "}|";
                        symbol = "o{";
                    }
                }
                
                sb.AppendLine($"{relation.Principal.Name} {principalSymbol}{relationSymbol}{symbol} {relation.Entity.Name}");
            }
            return sb.ToString();
        }
    }
}
