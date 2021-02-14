namespace Sped
{
    public struct Servico
    {
        public Servico(D190 d190)
        {
            ValorContabil = d190.ValorContabil;
            BaseCalculo = d190.BaseCalculo;
            Icms = d190.Icms;
            Cfop = d190.Cfop;
            Aliquota = d190.Aliquota;
        }

        public decimal ValorContabil { get; }
        public decimal BaseCalculo { get; }
        public decimal Icms { get; }
        public string Cfop { get; }
        public decimal Aliquota { get; }
    }
}