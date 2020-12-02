using GeometricService.Domain.Enums;

namespace GeometricService.Domain.Abstractions
{
    public interface IFigureResolverBuilder
    {
        IFigureResolverBuilder RegisterFigure(FigureType figureType, FigureFactory factory);

        IFigureResolver Build();
    }
}
