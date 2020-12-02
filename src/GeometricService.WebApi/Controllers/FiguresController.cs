using System.Threading;
using System.Threading.Tasks;
using GeometricService.Domain.Abstractions;
using GeometricService.Domain.Entities;
using GeometricService.Domain.Repositories;
using GeometricService.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GeometricService.WebApi.Controllers
{
    [ApiController]
    [Route("figure")]
    public class FiguresController : ControllerBase
    {
        private readonly IFiguresRepository _figuresRepository;
        private readonly IFigureResolver _figureResolver;

        public FiguresController(IFiguresRepository figuresRepository, IFigureResolver figureResolver)
        {
            _figuresRepository = figuresRepository;
            _figureResolver = figureResolver;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(400, Type = typeof(string))]
        public async Task<IActionResult> SaveFigure([FromBody] FigureDto figureDto, CancellationToken cancellationToken)
        {
            if (!_figureResolver.IsFigureTypeSupported(figureDto.Type))
                return BadRequest($"Figure type = {figureDto.Type} is not supported");

            if (!_figureResolver.TryParseFigureParameters(figureDto.Type, figureDto.Parameters, out var errorMessage))
                return BadRequest(errorMessage);

            var figure = new Figure()
            {
                Parameters = figureDto.Parameters,
                Type = figureDto.Type
            };

            var createdFigure = _figuresRepository.Add(figure);
            await _figuresRepository.SaveAsync(cancellationToken);
            return Ok(createdFigure.Id);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(404, Type = typeof(string))]
        public async Task<IActionResult> CalculateFigureArea([FromRoute] int id, CancellationToken cancellationToken)
        {
            var figureEntity = await _figuresRepository.GetByIdAsync(id, cancellationToken);

            if (figureEntity == null)
                return NotFound($"Figure with id = {id} not found");

            var targetFigure = _figureResolver.GetFigure(figureEntity.Type, figureEntity.Parameters);
            return Ok(targetFigure.Area);
        }
    }
}
