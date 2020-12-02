using GeometricService.Domain.Enums;

namespace GeometricService.Domain.Abstractions
{
    public interface IFigureResolver
    {
        bool IsFigureTypeSupported(FigureType type);

        bool TryParseFigureParameters(FigureType type, double[] parameters, out string errorMessage);

        IFigure GetFigure(FigureType type, double[] parameters);
    }
}
