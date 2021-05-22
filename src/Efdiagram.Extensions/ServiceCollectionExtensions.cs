using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Efdiagram.Extensions {
    public static class ServiceCollectionExtensions {

        public static void RegisterServices<T>(
            this IServiceCollection services,
            ServiceLifetime lifetime = ServiceLifetime.Singleton) {
            var typesFromAssemblies = 
                Assembly.GetEntryAssembly().DefinedTypes?.Where(type=> type.GetInterfaces().Contains(typeof(T)));
            foreach (var type in typesFromAssemblies) {
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
            }
        }
    }
}
