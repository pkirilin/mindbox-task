using GeometricService.Domain.Figures;
using System;
using Xunit;

namespace GeometricService.UnitTests
{
    public class FiguresTests
    {
        [Theory]
        [InlineData(10, 314.16)]
        [InlineData(15.6, 764.54)]
        public void Circle_ShouldReturnCorrectArea(double radius, double expectedArea)
        {
            // Arrange
            var circle = new Circle(radius);

            // Act
            var area = circle.Area;

            // Assert
            Assert.Equal(Math.Round(expectedArea, 2), Math.Round(area, 2));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void Circle_ShouldThrowArgumentException_WhenRadiusIsLessOrEqualZero(double radius)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var circle = new Circle(radius);
            });
        }

        [Theory]
        [InlineData(3, 5, 7, 6.5)]
        public void Triangle_ShouldReturnCorrectArea(double sideOne, double sideTwo, double sideThree, double expectedArea)
        {
            // Arrange
            var triangle = new Triangle(sideOne, sideTwo, sideThree);

            // Act
            var area = triangle.Area;

            // Assert
            Assert.Equal(Math.Round(expectedArea, 2), Math.Round(area, 2));
        }

        [Theory]
        [InlineData(3, 5, -7)]
        [InlineData(3, 0, 7)]
        [InlineData(-3, 5, -7)]
        [InlineData(0, -1, -2)]
        public void Triangle_ShouldThrowArgumentException_WhenAnyOfSidesIsLessOrEqualZero(double sideOne, double sideTwo, double sideThree)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var triangle = new Triangle(sideOne, sideTwo, sideThree);
            });
        }

        [Theory]
        [InlineData(3, 5, 100)]
        [InlineData(12.54, 35, 9.04)]
        public void Triangle_ShouldThrowArgumentException_WhenTriangleCannotExist(double sideOne, double sideTwo, double sideThree)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var triangle = new Triangle(sideOne, sideTwo, sideThree);
            });
        }
    }
}
