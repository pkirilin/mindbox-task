using GeometricService.Domain;
using GeometricService.Domain.Abstractions;
using GeometricService.Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace GeometricService.UnitTests
{
    public class FigureResolverTests
    {
        [Fact]
        public void IsFigureTypeSupported_ShouldReturnTrue_IfFigureTypeKeyExists()
        {
            // Arrange
            var factoryMock = new Mock<FigureFactory>();
            var factories = new Dictionary<FigureType, FigureFactory>()
            {
                [FigureType.Circle] = factoryMock.Object,
                [FigureType.Triangle] = factoryMock.Object
            };
            var resolver = new FigureResolver(factories);

            // Act
            var result = resolver.IsFigureTypeSupported(FigureType.Circle);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void TryParseFigureParameters_ShouldParseParameters_IfFigureTypeKeyExists()
        {
            // Arrange
            var factoryMock = new Mock<FigureFactory>();
            var factories = new Dictionary<FigureType, FigureFactory>()
            {
                [FigureType.Circle] = factoryMock.Object
            };
            var resolver = new FigureResolver(factories);
            var parameters = new double[] { 1, 2, 3 };

            // Act
            var result = resolver.TryParseFigureParameters(FigureType.Circle, parameters, out var message);

            // Assert
            factoryMock.Verify(f => f.TryParseFigureParameters(parameters, out message), Times.Once);
        }

        [Fact]
        public void TryParseFigureParameters_ShouldReturnFalse_IfFigureTypeKeyDoesNotExist()
        {
            // Arrange
            var factoryMock = new Mock<FigureFactory>();
            var factories = new Dictionary<FigureType, FigureFactory>()
            {
                [FigureType.Circle] = factoryMock.Object
            };
            var resolver = new FigureResolver(factories);
            var parameters = new double[] { 1, 2, 3 };

            // Act
            var result = resolver.TryParseFigureParameters(FigureType.Triangle, parameters, out var message);

            // Assert
            factoryMock.Verify(f => f.TryParseFigureParameters(parameters, out message), Times.Never);
            Assert.False(result);
        }

        [Fact]
        public void GetFigure_ShouldReturnFigure_IfFigureTypeKeyExists()
        {
            // Arrange
            var factoryMock = new Mock<FigureFactory>();
            var figureMock = new Mock<IFigure>();
            var factories = new Dictionary<FigureType, FigureFactory>()
            {
                [FigureType.Circle] = factoryMock.Object
            };
            var resolver = new FigureResolver(factories);
            var parameters = new double[] { 1, 2, 3 };

            factoryMock.Setup(f => f.CreateFigure(parameters)).Returns(figureMock.Object);

            // Act
            var figure = resolver.GetFigure(FigureType.Circle, parameters);

            // Assert
            factoryMock.Verify(f => f.CreateFigure(parameters), Times.Once);
            Assert.Equal(figureMock.Object, figure);
        }

        [Fact]
        public void GetFigure_ShouldThrowInvalidOperationException_IfFigureTypeKeyDoesNotExist()
        {
            var factoryMock = new Mock<FigureFactory>();
            var factories = new Dictionary<FigureType, FigureFactory>()
            {
                [FigureType.Circle] = factoryMock.Object
            };
            var resolver = new FigureResolver(factories);
            var parameters = new double[] { 1, 2, 3 };

            Assert.Throws<InvalidOperationException>(() =>
            {
                resolver.GetFigure(FigureType.Triangle, parameters);
            });
        }
    }
}
