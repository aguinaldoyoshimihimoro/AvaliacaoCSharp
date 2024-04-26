using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Interfaces
{
    public interface IContaCorrenteService
    {
        Task<List<contacorrente>> GetContaCorrente(int idCodCliente);

        Task<List<saldocontacorrenteTemp>> GetSaldoContaCorrente(int idCodCliente);
    }
}
