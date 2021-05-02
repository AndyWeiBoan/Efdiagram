using EfDiagram.Domain.Pocos;

namespace EfDiagram.Domain.Contracts {
    public interface IEfdiagramAnalyzer<T> {

        EfDaigramModel Resolve(T context);
    }
}
