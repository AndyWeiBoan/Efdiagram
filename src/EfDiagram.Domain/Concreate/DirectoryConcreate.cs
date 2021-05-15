using System.IO;
using System.Linq;
using EfDiagram.Domain.Contracts;

namespace EfDiagram.Domain.Concreate {
    public class DirectoryConcreate : IDirectory {

        string[] IDirectory.GetFilesPath(string pattern) {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var files = directory.GetFiles(pattern);
            var fullName = directory.FullName;
            return files?.Any() != true
                ? Enumerable.Empty<string>() as string[]
                : Directory.EnumerateFiles(fullName, pattern, SearchOption.AllDirectories) as string[];
        }
    }
}
