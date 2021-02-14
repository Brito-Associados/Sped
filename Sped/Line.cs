namespace Sped
{
    public struct Line
    {
        private const char SEPARATOR_PIPE = '|';

        public Line(string line)
        {
            Params = line.Split(SEPARATOR_PIPE);
        }

        public string Type => Params[1];
        public string[] Params { get; }
    }
}