using System.Collections.Generic;
using System.Linq;

namespace EfDiagram.Generator.Puml {
    public sealed class PumlSyntaxModel {
        public string Begin { get; set; } = "@startuml";
        public IList<string> Entities { get; set; }
        public string RelationShips { get; set; }
        public string End { get; set; } = "@enduml";

        public override string ToString() {
            return $@"
{this.Begin}
{string.Join("\r\n",this.Entities.ToArray())}
{this.RelationShips}
{this.End}";
        }
    }
}
