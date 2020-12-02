using GeometricService.Domain.Abstractions;
using GeometricService.Domain.Enums;
using System;
using System.Collections.Generic;

namespace GeometricService.Domain
{
    class FigureResolver : IFigureResolver
    {
        private readonly Dictionary<FigureType, FigureFactory> _figureFactories;

        public FigureResolver(Dictionary<FigureType, FigureFactory> figureFactories)
        {
            _figureFactories = figureFactories;
        }

        public bool IsFigureTypeSupported(FigureType type)
        {
            return _figureFactories.ContainsKey(type);
        }

        public bool TryParseFigureParameters(FigureType type, double[] parameters, out string errorMessage)
        {
            if (!_figureFactories.ContainsKey(type))
            {
                errorMessage = $"Figure with type = {type} is not supported";
                return false;
            }

            return _figureFactories[type].TryParseFigureParameters(parameters, out errorMessage);
        }

        public IFigure GetFigure(FigureType type, double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));
            if (!_figureFactories.ContainsKey(type))
                throw new InvalidOperationException($"Cannot resolve figure for type = {type}. Ensure that figure with type = {type} is supported");

            return _figureFactories[type].CreateFigure(parameters);
        }
    }
}
