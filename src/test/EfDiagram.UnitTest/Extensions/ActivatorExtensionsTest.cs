using Efdiagram.Extensions;
using EfDiagram.UnitTest.Analyzer.DBContext;
using Xunit;

namespace EfDiagram.UnitTest.Extensions {
    public class ActivatorExtensionsTest {

        [Fact]
        public void CreateDbContextTest() {
            // arrange
            var type = typeof(TestContext);

            // act
            var actual = ActivatorExtensions.CrteateDbContext(type);

            // assert
            Assert.NotNull(actual);
        }
    }
}
