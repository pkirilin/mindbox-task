using GeometricService.Domain.Enums;
using System;

namespace GeometricService.WebApi.Dtos
{
    public class FigureDto
    {
        public FigureType Type { get; set; } = FigureType.Circle;

        public double[] Parameters { get; set; } = Array.Empty<double>();
    }
}
