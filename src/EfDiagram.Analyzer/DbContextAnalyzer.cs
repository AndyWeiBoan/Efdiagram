using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EfDiagram.Domain;
using EfDiagram.Domain.Contracts;
using EfDiagram.Domain.Pocos;
using Microsoft.EntityFrameworkCore;

namespace EfDiagram.Analyzer {
    public class DbContextAnalyzer : IEfdiagramAnalyzer<DbContext> {
        
        EfDaigramModel IEfdiagramAnalyzer<DbContext>.Resolve(DbContext context) {

            var relationModel = context.Model.GetRelationalModel();
            var tables = relationModel.Tables;
            var foreignKeys = tables.SelectMany(p=> p.ForeignKeyConstraints).SelectMany(p=> p.Columns);
            var primaryKeys = tables.Select(p => p.PrimaryKey).SelectMany(p => p.Columns);
            var entities = tables.Select(table => new Entity {
                Name = table.Name,
                Columns = table?.Columns.Select(column => new Column {
                    Name = column.Name,
                    Type = column.StoreType,
                    IsForeignKey = foreignKeys?.Any(fk=> fk == column) == true,
                    IsPrimaryKey = primaryKeys?.Any(pk => pk == column) == true,
                })
            });

            var relaionShips = new List<TableRelationShip>();
            foreach (var foreignKey in tables.Where(t => t.ForeignKeyConstraints?.Any() == true).SelectMany(p=> p.ForeignKeyConstraints)) {
                var mappedForeignKey = foreignKey.MappedForeignKeys.First();
                var type = mappedForeignKey.DeclaringEntityType.ClrType;
                var principalType = mappedForeignKey.PrincipalToDependent.ClrType;
                relaionShips.Add(new TableRelationShip {
                    Entity = entities.FirstOrDefault(e => e.Name == foreignKey.Table.Name),
                    Principal = entities.FirstOrDefault(e => e.Name == foreignKey.PrincipalTable.Name),
                    Type = this.getRelationShipType(type, principalType),
                    Identifying = !(mappedForeignKey.DeleteBehavior == DeleteBehavior.Restrict
                    || mappedForeignKey.DeleteBehavior == DeleteBehavior.NoAction
                    || mappedForeignKey.DeleteBehavior == DeleteBehavior.ClientNoAction),
                });
            }

            return new EfDaigramModel { Entities = entities, RelationShips = relaionShips };
        }

        private RelationShipType getRelationShipType(Type type, Type principalType) {
            bool IsCollection(Type t) {
                return t.GetInterface(nameof(ICollection)) != default
                    || t.GetInterface(nameof(IEnumerable)) != default;
            }
            if (IsCollection(type) && IsCollection(principalType))
                return RelationShipType.ManyToMany;
            else if (!IsCollection(type) && IsCollection(principalType))
                return RelationShipType.OneToMany;
            else
                return RelationShipType.OneToOne;
        }
    }
}
