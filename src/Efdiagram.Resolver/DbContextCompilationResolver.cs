using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using EfDiagram.Domain.Contracts;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Diagnostics;
using Microsoft.Build.Locator;

namespace Efdiagram.Resolver {
    public class DbContextCompilationResolver : IDbContextResolver {

        private readonly Type _targetType = typeof(DbContext);
        private readonly ILogger<DbContextCompilationResolver> _logger;

        public DbContextCompilationResolver(ILogger<DbContextCompilationResolver> logger) => _logger = logger;

        IEnumerable<Type> IDbContextResolver.GetDbContextTypes(IEnumerable<string> solutions) {
            MSBuildLocator.RegisterDefaults();
            var resuls = new List<Type>();
            foreach (var solution in solutions) {
                try {                    
                    resuls.AddRange(this.ResovleDbContextType(solution));
                } catch (Exception ex) {
                    _logger.LogError(ex, $"Resovle {_targetType} from solution failed.(Path: {solution})");
                }
            }
            return resuls;
        }

        private IEnumerable<Type> ResovleDbContextType(string solutionPath) {
            using var ms = MSBuildWorkspace.Create();
            ms.WorkspaceFailed += (o, e) => this.OnWorkspaceFailed(o, e);
            var solution = ms.OpenSolutionAsync(solutionPath).Result;
            return solution.Projects
                .Select(project => this.GetAssemblyByProjectCompiled(project))
                .ToArray()
                .Where(assembly=> assembly != default)
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(_targetType) == true || type.IsAssignableFrom(_targetType));
        }

        private Assembly GetAssemblyByProjectCompiled(Project project) {
            var compilation = project.GetCompilationAsync().Result;
            if (null == compilation || string.IsNullOrEmpty(compilation.AssemblyName)) 
                return default(Assembly);
            using var mStream = new MemoryStream();
            var result = compilation.Emit(mStream);
            if (!result.Success) {
                this.Log(string.Join("\r\n", result.Diagnostics.Select(p=> p.GetMessage())));
                return default(Assembly);
            }
            mStream.Seek(0, SeekOrigin.Begin);
            return AssemblyLoadContext.Default.LoadFromStream(mStream);
        }

        private void OnWorkspaceFailed(object o, WorkspaceDiagnosticEventArgs e) {

            Log($"{e.Diagnostic.Kind}: {e.Diagnostic.Message}, {o}");
        }

        private void Log(string content) {
            Debug.WriteLine(content);

            _logger.LogWarning(content);
        }
    }
}
