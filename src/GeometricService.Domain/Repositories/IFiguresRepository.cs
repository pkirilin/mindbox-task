using GeometricService.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace GeometricService.Domain.Repositories
{
    public interface IFiguresRepository
    {
        Figure Add(Figure figure);

        ValueTask<Figure> GetByIdAsync(int figureId, CancellationToken cancellationToken);

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
