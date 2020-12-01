using System.Threading.Tasks;
using GeometricService.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GeometricService.WebApi.Controllers
{
    [ApiController]
    [Route("v1/figures")]
    public class FiguresController : ControllerBase
    {
        public FiguresController()
        {
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> SaveFigure([FromBody] FigureDto figureDto)
        {
            await Task.CompletedTask;
            return Ok();
        }

        [HttpGet("{figureId}/calculate-area")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CalculateFigureArea([FromRoute] int figureId)
        {
            await Task.CompletedTask;
            return Ok();
        }
    }
}
