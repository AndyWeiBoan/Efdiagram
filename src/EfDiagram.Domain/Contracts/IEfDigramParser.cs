using EfDiagram.Domain.Pocos;

namespace EfDiagram.Domain.Contracts {

    public interface IEfDigramParser {

        string GetResult(EfDaigramModel model);
    }
}
