using GeometricService.Domain.Entities;
using GeometricService.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace GeometricService.Infrastructure.Repositories
{
    class FiguresRepository : IFiguresRepository
    {
        private readonly GeometricServiceContext _context;

        public FiguresRepository(GeometricServiceContext context)
        {
            _context = context;
        }

        public Figure Add(Figure figure)
        {
            var entry = _context.Figures.Add(figure);
            return entry.Entity;
        }

        public ValueTask<Figure> GetByIdAsync(int figureId, CancellationToken cancellationToken)
        {
            return _context.Figures.FindAsync(new object[] { figureId }, cancellationToken);
        }

        public Task SaveAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
