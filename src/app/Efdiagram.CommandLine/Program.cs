using System.Reflection;
using Efdiagram.CommandLine.Commands;
using Efdiagram.CommandLine.Extensions;
using Microsoft.Extensions.Hosting;
namespace Efdiagram.CommandLine {
    class Program {
        static void Main(string[] args) {

            CreateHostBuilder(args).RunCommandLineApplicationAsync(args, app => {
                
                var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                app.HelpOption("-h|--help");
                app.VersionOption("-v|--version", $"Efdiagram version {version}");
                app.AddCommands<IEfdiagramCommand>();
            });

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
