namespace Sped
{
    public struct D190
    {
        private readonly Line _line;

        public D190(Line line)
        {
            _line = line;
        }

        public string Cfop => _line.Params[3];
        public decimal Aliquota => decimal.Parse(_line.Params[4]);
        public decimal ValorContabil => decimal.Parse(_line.Params[5]);
        public decimal BaseCalculo => decimal.Parse(_line.Params[6]);
        public decimal Icms => decimal.Parse(_line.Params[7]);
    }
}