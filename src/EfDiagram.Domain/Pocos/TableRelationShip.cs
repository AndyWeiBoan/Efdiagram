namespace EfDiagram.Domain.Pocos
{
    public enum RelationShipType
    {
        OneToOne,
        OneToMany,
        ManyToMany,
    }
    public sealed class TableRelationShip
    {
        public Entity Entity { get; set; }
        public Entity Principal { get; set; }
        public bool Identifying { get; set; }
        public RelationShipType Type { get; set; }
    }

    public sealed class TableRelationShipBrief {
        public string EntityName { get; set; }

        public string PrincipalName { get; set; }

        public RelationShipType Type { get; set; }
    }
}
