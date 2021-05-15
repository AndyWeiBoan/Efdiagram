using System.Collections.Generic;
using System.Linq;
using System.Text;
using EfDiagram.Domain;
using EfDiagram.Domain.Contracts;
using EfDiagram.Domain.Pocos;
using EfDiagram.Generator.Puml;

namespace EfDiagram.Parsers.PlantUml
{
    public sealed class PumlGenerator : IDigramGenerator {

        string IDigramGenerator.GetResult(EfDaigramModel model) {
            if (model.Entities?.Any() is not true) return string.Empty;
            var result = new PumlSyntaxModel { Entities = new  List<string>() };
            foreach (var e in model.Entities) {
                var columns = this.GetColumns(e.Columns);
                var entity = this.GetEntity(e.Name, columns);
                result.Entities.Add(entity);
            }
            if (model.RelationShips?.Any() is true)
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
            var columnDescription = new StringBuilder();
            var pkDescription = new StringBuilder();
            var fkDescription = new StringBuilder();
            foreach (var column in columns)
            {
                if (column.IsPrimaryKey && column.IsForeignKey)
                {
                    pkDescription.AppendLine($"    { column.Name}: { column.Type} <<FK>>");
                }
                else if (column.IsPrimaryKey)
                {
                    pkDescription.AppendLine($"    { column.Name}: { column.Type} ");
                    
                }
                else if (column.IsForeignKey)
                {
                    fkDescription.AppendLine($"    { column.Name}: { column.Type} ");
                }
                else
                {
                    columnDescription.AppendLine($"    { column.Name}: { column.Type}");
                }
            }
            pkDescription.Append($"---\r\n");

            return pkDescription.Append(fkDescription).Append(columnDescription).ToString();
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
