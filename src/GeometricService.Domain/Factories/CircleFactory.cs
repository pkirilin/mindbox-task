using GeometricService.Domain.Abstractions;
using GeometricService.Domain.Figures;
using System;

namespace GeometricService.Domain.Factories
{
    public class CircleFactory : FigureFactory
    {
        public override bool TryParseFigureParameters(double[] parameters, out string errorMessage)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (parameters.Length != 1)
            {
                errorMessage = "Circle parameters array must contain one value for radius";
                return false;
            }

            if (parameters[0] <= 0)
            {
                errorMessage = "Circle radius cannot be less or equal zero";
                return false;
            }

            errorMessage = null;
            return true;
        }

        public override IFigure CreateFigure(double[] parameters)
        {
            var radius = parameters[0];

            return new Circle(radius);
        }
    }
}
