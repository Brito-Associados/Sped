using System.Collections.Generic;

namespace Sped
{
    public class Servicos : List<Servico>
    {
        private const string D190 = "D190";

        public Servicos(string[] lines)
        {
            foreach (var line in lines)
            {
                var readLine = new Line(line);
                if (readLine.Type.Equals(D190))
                {
                    var servico = new Servico(new D190(readLine));
                    Add(servico);
                }
            }
        }
    }
}