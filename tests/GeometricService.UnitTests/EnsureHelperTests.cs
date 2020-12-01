using GeometricService.Domain;
using System;
using Xunit;

namespace GeometricService.UnitTests
{
    public class EnsureHelperTests
    {
        [Theory]
        [InlineData(-1, "First value", "param1", "First value cannot be less than or equal to zero (Parameter 'param1')")]
        [InlineData(0, "Second value", "param2", "Second value cannot be less than or equal to zero (Parameter 'param2')")]
        public void GreaterThanZero_ShouldThrowArgumentException_WhenValueIsLessThanOrEqualZero(
            double value,
            string targetName,
            string parameterName,
            string expectedErrorMessage)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                Ensure.GreaterThanZero(value, targetName, parameterName);
            });

            Assert.Equal(expectedErrorMessage, exception.Message);
        }
    }
}
