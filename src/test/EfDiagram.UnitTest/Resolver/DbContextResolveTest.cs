using Efdiagram.Resolver;
using EfDiagram.Domain.Contracts;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;


namespace EfDiagram.UnitTest.Resolver {

    public sealed class DbContextResolveTest {

        private readonly IDbContextResolver _target;
        private readonly ILogger<DbContextCompilationResolver> _logger;

        public DbContextResolveTest() {
            _logger = Substitute.For<ILoggerFactory>().CreateLogger<DbContextCompilationResolver>();
            _target = new DbContextCompilationResolver(_logger);
        }

        [Fact]
        public void ResolveTest() {
            // arrange
            var solutions = new string[] { @".\Stubs\TestSolution\Test.sln" };

            // act
            var types = _target.GetDbContextTypes(solutions);

            // assert
            Assert.NotEmpty(types);
        }
    }
}
