using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Efdiagram.Sample {
    class Program {
        static void Main(string[] args) {
            IServiceCollection services = new ServiceCollection();
            // Startup.cs finally :)
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            //configure console logging
            

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();

            logger.LogWarning("Logger is working!");

            // Get Service and call method
            var service = serviceProvider.GetService<Service>();
            logger.LogWarning(service.Run());
            Console.Read();
        }
    }
}
