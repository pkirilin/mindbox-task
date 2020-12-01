using GeometricService.Domain;
using Xunit;

namespace GeometricService.UnitTests
{
    public class FigureParametersConverterTests
    {
        [Fact]
        public void Converter_ShouldConvertSourceArrayToBytesAndBack_WithoutLosingData()
        {
            // Arrange
            var sourceArray = new double[] { 2.72, 67.15, 3, -56.3, 50, 0.12 };

            // Act
            var bytes = FigureParametersConverter.ConvertToBytes(sourceArray);
            var receivedArray = FigureParametersConverter.ConvertToDoubleArray(bytes);

            // Assert
            Assert.Equal(sourceArray, receivedArray);
        }
    }
}
