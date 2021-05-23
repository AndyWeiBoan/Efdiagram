using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Efdiagram.CommandLine.Extensions;
using Efdiagram.Extensions;
using EfDiagram.Domain.Contracts;
using EfDiagram.Domain.Pocos;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Efdiagram.CommandLine.Commands {

    public class ExecCommand : IEfdiagramCommand {

        private const string _commandName = "exec";
        private const string _description = @"Generate ER-diagram from dbcontext.";

        string IEfdiagramCommand.Name => _commandName;

        string IEfdiagramCommand.Description => _description;

        void IEfdiagramCommand.Register(CommandLineApplication app) {
            app.HelpOption("-h|--help");
            app.Description = _description;
            var solutionsOpt = app.Option("-s|--sln <solution>", "The solution files", CommandOptionType.SingleValue).Accepts(s=> s.ExistingFile());
            var outputOpt = app.Option("-o|--output <output>", "Out directory", CommandOptionType.SingleValue).Accepts(o=> o.LegalFilePath());

            app.OnExecute(() => {
                try {
                    var models = this.getEfDaigramModel(app, solutionsOpt.Values);
                    var generator = app.GetRequiredService<IDiagramGenerator>();
                    var results = models.Select(m => generator.GetResult(m));

                    if (!outputOpt.HasValue())
                        app.Out.WriteLine(string.Join("-------------", results.Select(r=> r.Content)));
                    else
                        app.Out.WriteToFiles(outputOpt.Value(), results);

                } catch (Exception ex) {
                    app.Out.WriteLine(ex.ToString());
                }
                finally {
                    app.Out.Flush();
                }
            });
        }

        private IList<EfDaigramModel> getEfDaigramModel(CommandLineApplication app, IEnumerable<string> solutionsOpt) {
            var directory = app.GetRequiredService<IDirectory>();
            var solutions = solutionsOpt?.Any() != true
                ? directory.GetFilesPath("*.sln")
                : solutionsOpt;

            var resovler = app.GetRequiredService<IDbContextResolver>();
            var types = resovler.GetDbContextTypes(solutions);
            var parser = app.GetRequiredService<IEfdiagramModelParser<DbContext>>();

            return types.Select(type => parser.GetResult(type.Name, ActivatorExtensions.CrteateDbContext(type))).ToList();
        }
    }
}
