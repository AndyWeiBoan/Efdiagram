using Efdiagram.CommandLine.Commands;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace Efdiagram.CommandLine.Extensions {
    internal static class CommandLineApplicationExtensions  {

        internal static CommandLineApplication AddCommands<T> (this CommandLineApplication application)
            where T : IEfdiagramCommand {
            var commands = application.GetServices<IEfdiagramCommand>();
            foreach (var command in commands) {
                application.Command(command.Name, app => command.Register(app));
            }
            return application;
        }
    }
}
