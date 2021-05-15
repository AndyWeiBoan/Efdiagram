using System.IO;
using System.Linq;
using EfDiagram.Domain.Contracts;

namespace EfDiagram.Domain.Concreate {
    public class DirectoryConcreate : IDirectory {

        private const string solutionFilePattern = "*.sln";

        string[] IDirectory.GetFilesPath(string pattern) {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var files = directory.GetFiles(solutionFilePattern);
            var fullName = directory.FullName;
            return files?.Any() != true
                ? Enumerable.Empty<string>() as string[]
                : Directory.EnumerateFiles(fullName, solutionFilePattern, SearchOption.AllDirectories) as string[];
        }
    }
}
