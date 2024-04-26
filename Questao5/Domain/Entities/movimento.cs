namespace Questao5.Domain.Entities
{
    public class movimento
    {
        public string idmovimento { get; set; }
        public string idcontacorrente { get; set; }
        public DateTime datamovimento { get; set; }
        public string tipomovimento { get; set; }
        public double valor { get; set; }
    }
}
