using System;
using System.Collections.Generic;
using System.IO;
using EfDiagram.Domain.Pocos;

namespace Efdiagram.CommandLine.Extensions {
    internal static class TextWriterExtensions {

        internal static void WriteToFiles(
            this TextWriter w, 
            string path, 
            IEnumerable<DiagramResult> results) {
            foreach (var result in results) {
                var directory = DateTime.Now.ToString("yyyyMMddhhmmss");
                Directory.CreateDirectory(Path.Combine(path, directory));
                var file = $"{result.DatabaseName}.{result.FileType}";
                using (w = File.CreateText(Path.Combine(path, directory, file))) {
                    w.WriteLine(result.Content);
                    w.Flush();
                    w.Close();
                }
            }
        }
    }
}
