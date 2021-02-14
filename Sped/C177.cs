namespace Sped
{
    public struct C177
    {
        private readonly Line _line;

        public C177(Line line)
        {
            _line = line;
        }

        public string Incentivo => _line.Params[2];
    }
}