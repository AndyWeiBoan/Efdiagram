using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
namespace EfDiagram.Domain.Pocos {
    public sealed class EfDaigramModel : IEquatable<EfDaigramModel> {

        public IEnumerable<Entity> Entities { get; set; }
        public IEnumerable<TableRelationShip> RelationShips { get; set; }

        public bool Equals([AllowNull] EfDaigramModel other) {
            return this.Entities.SequenceEqual(other.Entities);
        }

        public int GetHashCode([DisallowNull] EfDaigramModel obj) {
            return obj.GetHashCode();
        }
    }
}
