using GeometricService.Domain.Abstractions;
using GeometricService.Domain.Entities;
using GeometricService.Domain.Enums;
using GeometricService.Domain.Repositories;
using GeometricService.WebApi.Controllers;
using GeometricService.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GeometricService.UnitTests
{
    public class FiguresControllerTests
    {
        private readonly Mock<IFiguresRepository> _figuresRepositoryMock = new Mock<IFiguresRepository>();
        private readonly Mock<IFigureResolver> _figureResolverMock = new Mock<IFigureResolver>();

        public FiguresController Controller => new FiguresController(_figuresRepositoryMock.Object, _figureResolverMock.Object);

        [Fact]
        public async void SaveFigure_ShouldReturnOk_WhenFigureDataIsValid()
        {
            // Arrange
            var figureDto = new FigureDto()
            {
                Type = FigureType.Circle,
                Parameters = new double[] { 10 }
            };
            var createdFigure = new Figure();
            string tmp;

            _figureResolverMock.Setup(r => r.IsFigureTypeSupported(figureDto.Type)).Returns(true);
            _figureResolverMock.Setup(r => r.TryParseFigureParameters(figureDto.Type, figureDto.Parameters, out tmp)).Returns(true);
            _figuresRepositoryMock.Setup(r => r.Add(It.IsNotNull<Figure>())).Returns(createdFigure);

            // Act
            var response = await Controller.SaveFigure(figureDto, default);

            // Assert
            _figureResolverMock.Verify(r => r.IsFigureTypeSupported(figureDto.Type), Times.Once);
            _figureResolverMock.Verify(r => r.TryParseFigureParameters(figureDto.Type, figureDto.Parameters, out tmp), Times.Once);
            _figuresRepositoryMock.Verify(r => r.Add(It.IsNotNull<Figure>()), Times.Once);
            _figuresRepositoryMock.Verify(r => r.SaveAsync(default), Times.Once);
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async void SaveFigure_ShouldReturnBadRequest_WhenFigureTypeIsNotSupported()
        {
            // Arrange
            var figureDto = new FigureDto()
            {
                Type = FigureType.Circle,
                Parameters = new double[] { 10 }
            };
            var createdFigure = new Figure();
            string tmp;

            _figureResolverMock.Setup(r => r.IsFigureTypeSupported(figureDto.Type)).Returns(false);
            
            // Act
            var response = await Controller.SaveFigure(figureDto, default);

            // Assert
            _figureResolverMock.Verify(r => r.IsFigureTypeSupported(figureDto.Type), Times.Once);
            _figureResolverMock.Verify(r => r.TryParseFigureParameters(figureDto.Type, figureDto.Parameters, out tmp), Times.Never);
            _figuresRepositoryMock.Verify(r => r.Add(It.IsNotNull<Figure>()), Times.Never);
            _figuresRepositoryMock.Verify(r => r.SaveAsync(default), Times.Never);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async void SaveFigure_ShouldReturnBadRequest_WhenFigureParametersAreInvalid()
        {
            // Arrange
            var figureDto = new FigureDto()
            {
                Type = FigureType.Circle,
                Parameters = new double[] { 10 }
            };
            var createdFigure = new Figure();
            string tmp;

            _figureResolverMock.Setup(r => r.IsFigureTypeSupported(figureDto.Type)).Returns(true);
            _figureResolverMock.Setup(r => r.TryParseFigureParameters(figureDto.Type, figureDto.Parameters, out tmp)).Returns(false);

            // Act
            var response = await Controller.SaveFigure(figureDto, default);

            // Assert
            _figureResolverMock.Verify(r => r.IsFigureTypeSupported(figureDto.Type), Times.Once);
            _figureResolverMock.Verify(r => r.TryParseFigureParameters(figureDto.Type, figureDto.Parameters, out tmp), Times.Once);
            _figuresRepositoryMock.Verify(r => r.Add(It.IsNotNull<Figure>()), Times.Never);
            _figuresRepositoryMock.Verify(r => r.SaveAsync(default), Times.Never);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async void CalculateFigureArea_ShouldReturnOk_WhenFigureWithSpecifiedIdExists()
        {
            // Arrange
            var figureId = 10;
            var figure = new Figure()
            {
                Type = FigureType.Circle,
                Parameters = new double[] { 10 }
            };
            var resolvedFigureMock = new Mock<IFigure>();

            _figuresRepositoryMock
                .Setup(r => r.GetByIdAsync(figureId, default))
                .ReturnsAsync(figure);
            _figureResolverMock
                .Setup(r => r.GetFigure(figure.Type, figure.Parameters))
                .Returns(resolvedFigureMock.Object);

            // Act
            var response = await Controller.CalculateFigureArea(figureId, default);

            // Assert
            _figuresRepositoryMock.Verify(r => r.GetByIdAsync(figureId, default), Times.Once);
            _figureResolverMock.Verify(r => r.GetFigure(figure.Type, figure.Parameters), Times.Once);
            resolvedFigureMock.VerifyGet(f => f.Area, Times.Once);
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async void CalculateFigureArea_ShouldReturnNotFound_WhenFigureWithSpecifiedIdDoesNotExist()
        {
            // Arrange
            var figureId = 10;

            _figuresRepositoryMock.Setup(r => r.GetByIdAsync(figureId, default))
                .ReturnsAsync(null as Figure);

            // Act
            var response = await Controller.CalculateFigureArea(figureId, default);

            // Assert
            _figuresRepositoryMock.Verify(r => r.GetByIdAsync(figureId, default), Times.Once);
            _figureResolverMock.Verify(r => r.GetFigure(It.IsAny<FigureType>(), It.IsAny<double[]>()), Times.Never);
            Assert.IsType<NotFoundObjectResult>(response);
        }
    }
}
