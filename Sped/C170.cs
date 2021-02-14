namespace Sped
{
    public struct C170
    {
        private readonly Line _line;

        public C170(Line line)
        {
            _line = line;
        }

        public string CodProduto => _line.Params[3];
        public decimal ValorContabil => decimal.Parse(_line.Params[7]);
        public string Cfop => _line.Params[11];
        public decimal BaseCalculo => decimal.Parse(_line.Params[13]);
        public decimal Aliquota => decimal.Parse(_line.Params[14]);
        public decimal Icms => decimal.Parse(_line.Params[15]);
    }
}