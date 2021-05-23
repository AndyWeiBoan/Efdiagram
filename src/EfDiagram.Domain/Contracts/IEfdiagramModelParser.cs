using EfDiagram.Domain.Pocos;

namespace EfDiagram.Domain.Contracts {
    public interface IEfdiagramModelParser<T> {

        EfDaigramModel GetResult(string databaseName, T context);
    }
}
