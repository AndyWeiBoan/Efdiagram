using EfDiagram.Domain.Pocos;

namespace EfDiagram.Domain.Contracts {

    public interface IDigramGenerator {

        string GetResult(EfDaigramModel model);
    }
}
