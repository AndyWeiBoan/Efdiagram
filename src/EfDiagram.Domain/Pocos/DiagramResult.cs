namespace EfDiagram.Domain.Pocos {
    public sealed class DiagramResult {
        public string DatabaseName { get; set; }

        public string Content { get; set; }

        public FileType FileType { get; set; }
    }

    public enum FileType {
        puml,
        txt,
        json,
    }
}
