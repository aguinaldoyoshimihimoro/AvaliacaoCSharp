namespace Questao5.Domain.Entities
{
    public class idempotencia
    {
        public string chave_idempotencia { get; set; }
        public string? requisicao { get; set; }
        public string? resultado { get; set; }
    }
}
