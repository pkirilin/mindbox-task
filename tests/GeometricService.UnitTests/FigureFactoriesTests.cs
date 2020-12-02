using GeometricService.Domain.Factories;
using GeometricService.Domain.Figures;
using Xunit;

namespace GeometricService.UnitTests
{
    public class FigureFactoriesTests
    {
        [Fact]
        public void CircleFactory_ShouldCreateCircle_WithCorrectRadius()
        {
            // Arrange
            double radius = 123;
            var parameters = new double[] { radius };
            var factory = new CircleFactory();

            // Act
            var circle = factory.CreateFigure(parameters) as Circle;

            // Assert
            Assert.Equal(radius, circle.Radius);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        public void CircleFactory_ShouldValidateParametersLength(int parametersLength)
        {
            // Arrange
            var parameters = new double[parametersLength];
            var factory = new CircleFactory();

            // Act
            var result = factory.TryParseFigureParameters(parameters, out var message);

            // Assert
            Assert.False(result);
            Assert.Equal("Circle parameters array must contain one value for radius", message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void CircleFactory_ShouldValidateRadiusValue(double radius)
        {
            // Arrange
            var parameters = new double[] { radius };
            var factory = new CircleFactory();

            // Act
            var result = factory.TryParseFigureParameters(parameters, out var message);

            // Assert
            Assert.False(result);
            Assert.Equal("Circle radius cannot be less or equal zero", message);
        }

        [Fact]
        public void TriangleFactory_ShouldCreateTriangle_WithCorrectSides()
        {
            // Arrange
            double sideOne = 3;
            double sideTwo = 5;
            double sideThree = 7;
            var parameters = new double[] { sideOne, sideTwo, sideThree };
            var factory = new TriangleFactory();

            // Act
            var triangle = factory.CreateFigure(parameters) as Triangle;

            // Assert
            Assert.Equal(sideOne, triangle.SideOne);
            Assert.Equal(sideTwo, triangle.SideTwo);
            Assert.Equal(sideThree, triangle.SideThree);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(4)]
        public void TriangleFactory_ShouldValidateParametersLength(int parametersLength)
        {
            // Arrange
            var parameters = new double[parametersLength];
            var factory = new TriangleFactory();

            // Act
            var result = factory.TryParseFigureParameters(parameters, out var message);

            // Assert
            Assert.False(result);
            Assert.Equal("Triangle parameters array must contain 3 values for triangle sides", message);
        }

        [Theory]
        [InlineData(3, 5, -7)]
        [InlineData(3, 0, 7)]
        [InlineData(-3, 5, -7)]
        [InlineData(0, -1, -2)]
        public void TriangleFactory_ShouldValidateTriangleSideValues(double sideOne, double sideTwo, double sideThree)
        {
            // Arrange
            var parameters = new double[] { sideOne, sideTwo, sideThree };
            var factory = new TriangleFactory();

            // Act
            var result = factory.TryParseFigureParameters(parameters, out var message);

            // Assert
            Assert.False(result);
            Assert.Equal("Triangle sides cannot be less or equal zero", message);
        }
    }
}
