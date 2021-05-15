using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using EfDiagram.Domain.Contracts;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Diagnostics;

namespace Efdiagram.Resolver {
    public class DbContextCompilationResolver : IDbContextResolver {

        private readonly IDirectory _dir;
        private readonly ILogger<DbContextCompilationResolver> _logger;
        private readonly CSharpCompilationOptions _compilationOptions;
        public DbContextCompilationResolver(
            ILogger<DbContextCompilationResolver> logger,
            IDirectory dir) {
            _compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            _logger = logger;
            _dir = dir;
        }

        IEnumerable<Type> IDbContextResolver.GetDbContextTypes() {
            var resuls = new List<Type>();
            foreach (var solution in _dir.GetFilesPath("*.sln")) {
                resuls.AddRange(this.ResovleDbContextType(solution));
            }
            return resuls;
        }

        private IEnumerable<Type> ResovleDbContextType(string solutionPath) { 
            using var ms = MSBuildWorkspace.Create();
            ms.WorkspaceFailed += (o, e) => Debug.WriteLine(e.Diagnostic.Message, e.Diagnostic.Kind);
            var solution = ms.OpenSolutionAsync(solutionPath).Result;
            var projectGraph = solution.GetProjectDependencyGraph();
            var projects = projectGraph.GetTopologicallySortedProjects();
            var assemblies = new List<Assembly>();
            foreach (ProjectId projectId in projectGraph.GetTopologicallySortedProjects()) {
                var compilation = solution.GetProject(projectId).GetCompilationAsync().Result;
                compilation = compilation.WithOptions(_compilationOptions);
                if (null == compilation || string.IsNullOrEmpty(compilation.AssemblyName)) continue;
                using var mStream = new MemoryStream();
                var result = compilation.Emit(mStream);
                if (!result.Success) continue;
                mStream.Seek(0, SeekOrigin.Begin);
                assemblies.Add(AssemblyLoadContext.Default.LoadFromStream(mStream));
            }
            foreach (var type in assemblies.SelectMany(p => p.GetTypes()))
            {               
                if (type.IsSubclassOf(typeof(DbContext)) == true || type.IsAssignableFrom(typeof(DbContext))) 
                    yield return type;
            }
        }
    }
}
