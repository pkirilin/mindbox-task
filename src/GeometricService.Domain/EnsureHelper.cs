using System;

namespace GeometricService.Domain
{
    public static class Ensure
    {
        public static void GreaterThanZero(double value, string targetName, string parameterName)
        {
            if (value <= 0)
                throw new ArgumentException($"{targetName} cannot be less than or equal to zero", parameterName);
        }
    }
}
