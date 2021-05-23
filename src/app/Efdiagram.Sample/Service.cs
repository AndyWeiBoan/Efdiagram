using Efdiagram.Extensions;
using EfDiagram.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Efdiagram.Sample {
    public class Service {
        private readonly IEfdiagramModelParser<DbContext> parser;
        private readonly IDbContextResolver resolver;
        private readonly IDiagramGenerator generator;
        private readonly IDirectory dir;

        public Service(
            IEfdiagramModelParser<DbContext> parser,
            IDbContextResolver resolver,
            IDiagramGenerator generator,
            IDirectory dir) {
            this.parser = parser;
            this.resolver = resolver;
            this.generator = generator;
            this.dir = dir;
        }

        public string Run() {
            var solutions = this.dir.GetFilesPath("*.sln");
            var types = resolver.GetDbContextTypes(solutions);
            foreach (var type in types) {
                var efdiagramModel = parser.GetResult(type.Name, ActivatorExtensions.CrteateDbContext(type));
                var generated = this.generator.GetResult(efdiagramModel);
                if (!string.IsNullOrEmpty(generated.Content))
                    return generated.Content;
            }
            return "Nothing generated.";
        }
    }
}
