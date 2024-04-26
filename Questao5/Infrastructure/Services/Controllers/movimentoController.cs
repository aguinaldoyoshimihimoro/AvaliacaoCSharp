using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Questao5.Infrastructure.Configurations;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class movimentoController : ControllerBase
    {

        private readonly ILogger<movimentoController> _logger;

        public movimentoController(ILogger<movimentoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ActionName("InsereMovimento")]
        public async Task<ActionResult> InsertMovimento(int idContaCorrente, double valormovimento, string tipomovimento)
        {
            try
            {
                if (idContaCorrente == default(int))
                    return BadRequest();

                movimentoService movimento = new movimentoService();
                var movimentosVM = await movimento.InsertMovimento(idContaCorrente, valormovimento, tipomovimento);

                return Ok(movimentosVM);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error inserting valor: {ex.Message}.");
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new Microsoft.AspNetCore.Mvc.ObjectResult(ex.Message);
            }
        }
    }
}