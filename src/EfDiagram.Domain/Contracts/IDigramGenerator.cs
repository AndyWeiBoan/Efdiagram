using EfDiagram.Domain.Pocos;

namespace EfDiagram.Domain.Contracts {

    public interface IDiagramGenerator {

        DiagramResult GetResult(EfDaigramModel model);
    }
}
