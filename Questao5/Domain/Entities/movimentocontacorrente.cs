namespace Questao5.Domain.Entities
{
    public class movimentocontacorrente
    {
        public double valormovimento { get; set; }
        public int numero { get; set; }
        public string tipomovimento { get; set; }
        public DateTime datamovimento { get; set; }
        public int status { get; set; }
        public string descricao { get; set; }
    }
}
