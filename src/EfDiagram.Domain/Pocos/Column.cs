using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace EfDiagram.Domain {
    public sealed class Column : IEquatable<Column> {
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }        

        public bool Equals([AllowNull] Column other) {
            return this.Name == other.Name
                && this.Type.Contains(other.Type)
                && this.IsForeignKey == other.IsForeignKey
                && this.IsPrimaryKey == other.IsPrimaryKey;
        }

        public int GetHashCode([DisallowNull] Column obj) {
            return obj.GetHashCode();
        }
    }
}
