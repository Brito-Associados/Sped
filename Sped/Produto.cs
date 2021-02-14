namespace Sped
{
    public struct Produto
    {
        public Produto(C170 c170, C177 c177)
        {
            ValorContabil = c170.ValorContabil;
            BaseCalculo = c170.BaseCalculo;
            Icms = c170.Icms;
            Cfop = c170.Cfop;
            Aliquota = c170.Aliquota;
            Apuracao = c177.Incentivo;
            CodProduto = c170.CodProduto;
        }

        public string CodProduto { get; }
        public decimal ValorContabil { get; }
        public decimal BaseCalculo { get; }
        public decimal Icms { get; }
        public string Cfop { get; }
        public decimal Aliquota { get; }
        public string Apuracao { get; }
    }
}