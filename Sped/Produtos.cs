using System.Collections.Generic;

namespace Sped
{
    public class Produtos : List<Produto>
    {
        private const string C170 = "C170";

        public Produtos(string[] lines)
        {
            for (int i = 0; i < lines.Length - 1; i++)
            {
                var readLine = new Line(lines[i]);

                if (readLine.Type.Equals(C170))
                {
                    var nextLine = new Line(lines[i + 1]);
                    var produto = new Produto(new C170(readLine), new C177(nextLine));
                    Add(produto);
                }
            }
        }
    }
}