using GeometricService.Domain.Abstractions;
using GeometricService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GeometricService.UnitTests")]

namespace GeometricService.Domain
{
    class FigureResolverBuilder : IFigureResolverBuilder
    {
        private readonly Dictionary<FigureType, FigureFactory> _figureFactories;

        public FigureResolverBuilder()
        {
            _figureFactories = new Dictionary<FigureType, FigureFactory>();
        }

        public IFigureResolver Build()
        {
            return new FigureResolver(_figureFactories);
        }

        public IFigureResolverBuilder RegisterFigure(FigureType figureType, FigureFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));
            if (_figureFactories.ContainsKey(figureType))
                throw new InvalidOperationException($"Figure with type = '{figureType}' is already registered");
            
            _figureFactories.Add(figureType, factory);
            return this;
        }
    }
}
