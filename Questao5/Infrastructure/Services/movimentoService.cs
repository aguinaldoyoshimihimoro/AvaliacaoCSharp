using Questao5.Domain.Entities;
using Questao5.Infrastructure.Configurations;
using Questao5.Infrastructure.Interfaces;

namespace Questao5.Infrastructure.Services
{
    public class movimentoService : IMovimentoService
    {
        public async Task<List<movimentocontacorrente>> InsertMovimento(int idContaCorrente, double valormovimento, string tipomovimento)
        {
            MovimentoContext movimento = new MovimentoContext();
            var movimentos = movimento.InsertMovimento(idContaCorrente, valormovimento, tipomovimento);

            if (movimentos == null)
                return new List<movimentocontacorrente>();

            var movimentosVM = movimentos.Select(
                cc => new movimentocontacorrente
                {
                    valormovimento = cc.valormovimento,
                    numero = cc.numero,
                    tipomovimento = cc.tipomovimento,
                    datamovimento = cc.datamovimento,
                    status = cc.status,
                    descricao = cc.descricao
                }
                ).ToList();

            return movimentosVM;
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

