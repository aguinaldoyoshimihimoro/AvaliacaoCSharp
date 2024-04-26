namespace Questao5.Domain.Entities
{
    public class saldocontacorrente
    {
        public double saldo { get; set; }
        public int numero { get; set; }
        public string nome { get; set; }
        public DateTime dataconsulta { get; set;}
    }

    public class saldocontacorrenteTemp
    {
        public double saldo { get; set; }
        public int numero { get; set; }
        public string nome { get; set; }
        public DateTime dataconsulta { get; set; }
        public int status { get; set; }
    }
}
