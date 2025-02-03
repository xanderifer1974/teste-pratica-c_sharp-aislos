using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {

        public int Numero { get; private set; }
        public string Titular { get; set; }
        public double Saldo { get; private set; }

        private const double TaxaSaque = 3.50;

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
            Saldo = 0.0;
        }

        public ContaBancaria(int numero, string titular, double depositoInicial) : this(numero, titular)
        {
            Depositar(depositoInicial);
        }


        public void Depositar(double quantia)
        {
            Saldo += quantia;
        }
        public void Sacar(double quantia)
        {
            Saldo -= quantia + TaxaSaque;
        }
        public override string ToString()
        {
            return $"Conta {Numero}, Titular: {Titular}, Saldo: $ {Saldo:F2}";
        }

    }
}
