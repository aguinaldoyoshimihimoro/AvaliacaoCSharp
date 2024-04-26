using System;
using System.Globalization;

namespace Questao1
{

    public struct ContaBancaria
    {
        private int numero;
        private string titular;
        private char resp;
        private double saldo;

        public ContaBancaria(int numero, string titular) : this()
        {
            this.numero = numero;
            this.titular = titular;
        }

        public ContaBancaria(int numero, string titular, double depositoInicial) : this()
        {
            this.numero = numero;
            this.titular = titular;
            this.saldo = depositoInicial;
        }

        public int Numero
        {
            set { this.numero = value; }
            get { return this.numero; }
        }

        public string Titular
        {
            set { this.titular = value; }
            get { return this.titular; }
        }

        public char Resp
        {
            set { this.resp = value; }
            get { return this.resp; }
        }

        public double Saldo
        {
            set { this.saldo = value; }
            get { return this.saldo; }
        }
                
        public void SaldoInicial()
        {
            this.saldo = 0;
        }

        public double Deposito(double deposito)
        {
            this.saldo = this.saldo + deposito;
            return this.saldo;
        }

        public double Saque(double saque)
        {
            this.saldo = this.saldo - saque - 3.50;
            return this.saldo;
        }

        public double ConsultaSaldo()
        {
            return this.saldo;
        }
    }
}
