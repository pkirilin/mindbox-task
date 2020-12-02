namespace GeometricService.Domain.Abstractions
{
    public abstract class FigureFactory
    {
        public abstract bool TryParseFigureParameters(double[] parameters, out string errorMessage);

        public abstract IFigure CreateFigure(double[] parameters);
    }
}
