using GeometricService.Domain.Abstractions;
using System;

namespace GeometricService.Domain.Figures
{
    public class Circle : IFigure
    {
        private readonly double _radius;

        public Circle(double radius)
        {
            Ensure.GreaterThanZero(radius, "Circle radius", nameof(radius));
            _radius = radius;
        }

        public double Area => Math.PI * Math.Pow(_radius, 2);

        public double Radius => _radius;
    }
}
