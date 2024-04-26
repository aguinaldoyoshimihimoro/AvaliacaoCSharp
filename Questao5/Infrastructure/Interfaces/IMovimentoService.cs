using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Interfaces
{
    public interface IMovimentoService
    {
        Task<List<movimentocontacorrente>> InsertMovimento(int idContaCorrente, double valormovimento, string tipomovimento);
    }
}

