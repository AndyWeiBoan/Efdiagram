using EfDiagram.Domain.Pocos;

namespace EfDiagram.Domain.Contracts {

    public interface IDiagramGenerator {

        string GetResult(EfDaigramModel model);
    }
}
