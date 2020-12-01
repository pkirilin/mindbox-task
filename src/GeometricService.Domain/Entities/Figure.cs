using GeometricService.Domain.Enums;

namespace GeometricService.Domain.Entities
{
    public class Figure
    {
        public int Id { get; set; }

        public double[] Parameters { get; set; }

        public FigureType Type { get; set; }
    }
}
