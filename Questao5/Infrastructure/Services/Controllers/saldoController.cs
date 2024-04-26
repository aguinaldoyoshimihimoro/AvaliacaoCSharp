using FluentAssertions.Common;
using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Configurations;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class saldoController : ControllerBase
    {

        private readonly ILogger<saldoController> _logger;

        public saldoController(ILogger<saldoController> logger)
        {
            _logger = logger;
        }



        [HttpGet]
        [ActionName("ConsultaSaldoContaCorrente")]
        public async Task<ActionResult> GetSaldoContaCorrente(int idcontacorrente)
        {
            try
            {
                if (idcontacorrente == default(int))
                    return BadRequest();

                contacorrenteService contacorrente = new contacorrenteService();
                var clientesVM = await contacorrente.GetSaldoContaCorrente(idcontacorrente);

                foreach (saldocontacorrenteTemp saldocc in clientesVM)
                {
                    if (saldocc.status == 0)
                    {
                        this._logger.LogError($"Warning Status Conta Corrente inativa: {saldocc.nome}.");
                        Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                        return new Microsoft.AspNetCore.Mvc.ObjectResult("Warning Status Conta Corrente inativa");
                    }
                }

                var contacorrentesVM = clientesVM.Select(
                cc => new saldocontacorrente
                    {
                        saldo = cc.saldo,
                        numero = cc.numero,
                        nome = cc.nome,
                        dataconsulta = cc.dataconsulta,
                    }
                ).ToList();



                return Ok(contacorrentesVM);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error getting Saldo: {ex.Message}.");
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new Microsoft.AspNetCore.Mvc.ObjectResult(ex.Message);
            }
        }

    }
}