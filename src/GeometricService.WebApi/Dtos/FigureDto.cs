using GeometricService.Domain.Enums;

namespace GeometricService.WebApi.Dtos
{
    public class FigureDto
    {
        public FigureType Type { get; set; }

        public double[] Parameters { get; set; }
    }
}
