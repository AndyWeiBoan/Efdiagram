using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Efdiagram.Resolver;
using EfDiagram.Domain.Concreate;
using EfDiagram.Domain.Contracts;
using EfDiagram.Parsers;
using EfDiagram.Parsers.PlantUml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Efdiagram.Sample {
    public class Startup {

        IConfigurationRoot Configuration { get; }

        public Startup() {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddLogging(config => config.AddConsole());
            services.AddSingleton(Configuration);
            services.AddSingleton(typeof(Service));
            services.AddSingleton<IDiagramGenerator, PumlGenerator>();
            services.AddSingleton<IDirectory, DirectoryConcreate>();
            services.AddSingleton<IEfdiagramModelParser<DbContext>, DbContextParser>();
            services.AddSingleton<IDbContextResolver, DbContextCompilationResolver>();
        }
    }
}
