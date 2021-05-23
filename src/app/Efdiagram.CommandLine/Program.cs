using System.Reflection;
using System.Threading.Tasks;
using Efdiagram.CommandLine.Commands;
using Efdiagram.CommandLine.Extensions;
using Microsoft.Extensions.Hosting;
namespace Efdiagram.CommandLine {
    class Program {
        static async Task Main(string[] args) {
            await Host.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .RunCommandLineApplicationAsync(args, app => {
                    var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    app.HelpOption("-h|--help");
                    app.VersionOption("-v|--version", $"Efdiagram version {version}");
                    app.AddCommands<IEfdiagramCommand>();
                });
        }
    }
}
