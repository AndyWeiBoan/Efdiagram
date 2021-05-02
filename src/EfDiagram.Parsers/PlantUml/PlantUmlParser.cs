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
                sb.AppendLine($"    {column.Name}: {column.Type}");
                sb.Append($"{( column.IsPrimaryKey ? "---\r\n" : "" )}");
                sb.Append($"{( column.IsForeignKey ? "<<FK>>" : "" )}");
            }
            return sb.ToString();
        }
    }
}
