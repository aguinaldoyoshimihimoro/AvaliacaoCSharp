using Questao5.Domain.Entities;
using Questao5.Infrastructure.Configurations;
using Questao5.Infrastructure.Interfaces;

namespace Questao5.Infrastructure.Services
{
    public class contacorrenteService : IContaCorrenteService
    {
        public async Task<List<contacorrente>> GetContaCorrente(int idContaCorrente)
        {
            ContaCorrenteContext contacorrente = new ContaCorrenteContext();
            var contacorrentes = contacorrente.GetContaCorrente(idContaCorrente);
                        
            if (contacorrentes == null)
                return new List<contacorrente>();

            var contacorrentesVM = contacorrentes.Select(
                cc => new contacorrente
                {
                    idcontacorrente = cc.idcontacorrente,
                    numero = cc.numero,
                    nome = cc.nome,
                    ativo = cc.ativo
                }
            ).ToList();

            return contacorrentesVM;
        }


        public async Task<List<saldocontacorrenteTemp>> GetSaldoContaCorrente(int idContaCorrente)
        {
            ContaCorrenteContext contacorrente = new ContaCorrenteContext();
            var contacorrentes = contacorrente.GetSaldoContaCorrente(idContaCorrente);

            if (contacorrentes == null)
                return new List<saldocontacorrenteTemp>();

            var contacorrentesVM = contacorrentes.Select(
                cc => new saldocontacorrenteTemp
                {
                    saldo = cc.saldo,
                    numero = cc.numero,
                    nome = cc.nome,
                    dataconsulta = cc.dataconsulta,
                    status = cc.status
                }
            ).ToList();

            return contacorrentesVM;
        }
    }
}

