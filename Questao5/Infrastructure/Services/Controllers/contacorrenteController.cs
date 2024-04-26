using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Questao5.Infrastructure.Configurations;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class contacorrenteController : ControllerBase
    {
        
        private readonly ILogger<contacorrenteController> _logger;
        
        public contacorrenteController(ILogger<contacorrenteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ActionName("ConsultaContaCorrente")]
        public async Task<ActionResult> GetContaCorrente(int idcontacorrente)
        {
            try
            {
                if (idcontacorrente == default(int))
                    return BadRequest();

                contacorrenteService contacorrente = new contacorrenteService();
                var clientesVM = await contacorrente.GetContaCorrente(idcontacorrente);
                
                return Ok(clientesVM);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error getting Conta Corrente: {ex.Message}.");
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new Microsoft.AspNetCore.Mvc.ObjectResult(ex.Message);
            }
        }
    }
}