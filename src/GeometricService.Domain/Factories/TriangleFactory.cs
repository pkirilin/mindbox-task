using GeometricService.Domain.Abstractions;
using GeometricService.Domain.Figures;
using System;

namespace GeometricService.Domain.Factories
{
    public class TriangleFactory : FigureFactory
    {
        public override bool TryParseFigureParameters(double[] parameters, out string errorMessage)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (parameters.Length != 3)
            {
                errorMessage = "Triangle parameters array must contain 3 values for triangle sides";
                return false;
            }

            if (parameters[0] <= 0 || parameters[1] <= 0 || parameters[2] <= 0)
            {
                errorMessage = "Triangle sides cannot be less or equal zero";
                return false;
            }

            errorMessage = null;
            return true;
        }

        public override IFigure CreateFigure(double[] parameters)
        {
            var sideOne = parameters[0];
            var sideTwo = parameters[1];
            var sideThree = parameters[2];

            return new Triangle(sideOne, sideTwo, sideThree);
        }
    }
}
