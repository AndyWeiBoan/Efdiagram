using Efdiagram.CommandLine.Commands;
using Efdiagram.Extensions;
using Efdiagram.Resolver;
using EfDiagram.Domain.Concrete;
using EfDiagram.Domain.Contracts;
using EfDiagram.Parsers;
using EfDiagram.Parsers.PlantUml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Efdiagram.CommandLine {
    public class Startup {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services) {
            // Configure your services here
            services.RegisterServices<IEfdiagramCommand>();
            services.AddSingleton<IDiagramGenerator, PumlGenerator>();
            services.AddSingleton<IDirectory, DirectoryConcreate>();
            services.AddSingleton<IEfdiagramModelParser<DbContext>, DbContextParser>();
            services.AddSingleton<IDbContextResolver, DbContextCompilationResolver>();
        }
    }
}
