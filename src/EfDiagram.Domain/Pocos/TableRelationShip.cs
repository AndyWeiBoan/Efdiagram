namespace EfDiagram.Domain.Pocos
{
    public enum RelationShipType
    {
        OneToOne,
        OneToMany,

    }
    public sealed class TableRelationShip
    {
        public Entity Entity { get; set; }

        public RelationShipType Type { get; set; }
    }
}
