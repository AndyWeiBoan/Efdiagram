using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
namespace EfDiagram.Domain {
    public class Entity : IEquatable<Entity>
    {
        public string Name { get; set; }

        public IEnumerable<Column> Columns { get; set; }

        public bool Equals([AllowNull] Entity other) {
            return this.Name == other.Name
                && this.Columns.SequenceEqual(other.Columns);
        }

        public int GetHashCode([DisallowNull] Entity obj) {
            return obj.GetHashCode();
        } 
    }
}
