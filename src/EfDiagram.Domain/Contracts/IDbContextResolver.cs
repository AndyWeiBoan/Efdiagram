using System;
using System.Collections.Generic;

namespace EfDiagram.Domain.Contracts {
    public interface IDbContextResolver {
        IEnumerable<Type> GetDbContextTypes(IEnumerable<string> solutions);
    }
}
