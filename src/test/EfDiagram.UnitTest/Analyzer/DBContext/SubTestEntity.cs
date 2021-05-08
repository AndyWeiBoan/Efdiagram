using System;

namespace EfDiagram.UnitTest.Analyzer.DBContext
{
    public class SubTestEntity
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public TestEntity Test { get; set; }
    }
}
