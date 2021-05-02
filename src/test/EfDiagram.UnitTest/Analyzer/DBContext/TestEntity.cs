using System;

namespace EfDiagram.UnitTest.Analyzer.DBContext {
    public class TestEntity {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
