using System;
using System.Collections.Generic;
using System.Text;

namespace EfDiagram.Domain.Contracts {
    public interface IDirectory {
        string[] GetFilesPath(string pattern);
    }
}
