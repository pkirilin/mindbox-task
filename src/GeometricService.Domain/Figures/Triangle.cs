using GeometricService.Domain.Abstractions;
using System;

namespace GeometricService.Domain.Figures
{
    public class Triangle : IFigure
    {
        private readonly double _sideOne;
        private readonly double _sideTwo;
        private readonly double _sideThree;

        private const string AssertionTargetName = "Triangle side";

        public Triangle(double sideOne, double sideTwo, double sideThree)
        {
            Ensure.GreaterThanZero(sideOne, AssertionTargetName, nameof(sideOne));
            Ensure.GreaterThanZero(sideTwo, AssertionTargetName, nameof(sideTwo));
            Ensure.GreaterThanZero(sideThree, AssertionTargetName, nameof(sideThree));

            if (!IsTriangleExists(sideOne, sideTwo, sideThree))
                throw new ArgumentException($"Such triangle does not exists ({sideOne}, {sideTwo}, {sideThree})");

            _sideOne = sideOne;
            _sideTwo = sideTwo;
            _sideThree = sideThree;
        }

        private static bool IsTriangleExists(double sideOne, double sideTwo, double sideThree)
        {
            return sideOne + sideTwo > sideThree
                && sideOne + sideThree > sideTwo
                && sideTwo + sideThree > sideOne;
        }

        public double Area
        {
            get
            {
                var halfPerimeter = (_sideOne + _sideTwo + _sideThree) / 2;
                var halfPerimeterWithoutSideOne = halfPerimeter - _sideOne;
                var halfPerimeterWithoutSideTwo = halfPerimeter - _sideTwo;
                var halfPerimeterWithoutSideThree = halfPerimeter - _sideThree;

                return Math.Sqrt(halfPerimeter
                    * halfPerimeterWithoutSideOne
                    * halfPerimeterWithoutSideTwo
                    * halfPerimeterWithoutSideThree);
            }
        }

        public double SideOne => _sideOne;

        public double SideTwo => _sideTwo;

        public double SideThree => _sideThree;
    }
}
