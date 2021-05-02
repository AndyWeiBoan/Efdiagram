using System.Linq;
using EfDiagram.Domain;
using EfDiagram.Domain.Contracts;
using EfDiagram.Domain.Pocos;
using Microsoft.EntityFrameworkCore;

namespace EfDiagram.Analyzer {
    public class DbContextAnalyzer : IEfdiagramAnalyzer<DbContext> {

        EfDaigramModel IEfdiagramAnalyzer<DbContext>.Resolve(DbContext context) {

            return new EfDaigramModel {
                Entities = context.Model.GetRelationalModel()?.Tables.Select(p => new Entity {
                    Name = p.Name,
                    Columns = p?.Columns.Select(m => new Column {
                        Name = m.Name,
                        Type = m.StoreType,
                        IsForeignKey = p.ForeignKeyConstraints.Any(a=> a.Columns.Any(b=> b == m)),
                        IsPrimaryKey = p.PrimaryKey.Columns.Any(a=> a == m),
                    }).OrderByDescending(p => p.IsPrimaryKey).ThenByDescending(p => p.Name)
                })
            };
        }
    }
}
