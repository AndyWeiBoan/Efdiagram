using System.IO;
using System.Linq;
using EfDiagram.Domain.Contracts;

namespace EfDiagram.Domain.Concreate {
    public class DirectoryConcreate : IDirectory {

        string[] IDirectory.GetFilesPath(string pattern) {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            return directory
                .GetFiles(pattern, SearchOption.AllDirectories)
                .Select(file=> file.FullName).ToArray();
        }
    }
}
