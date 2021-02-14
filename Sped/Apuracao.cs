using System.Collections.Generic;
using System.Linq;

namespace Sped
{
    public class Apuracao
    {
        private readonly string _idApuracao;
        public Produtos Produtos { get; }

        public Servicos Servicos { get; }

        public Apuracao(Produtos produtos, string idApuracao = null)
        {
            Produtos = produtos;
            _idApuracao = idApuracao;
        }

        public Apuracao(Servicos servicos)
        {
            Servicos = servicos;
        }

        public object ObterProdutosPorApuracao()
        {
            IEnumerable<Produto> produtos;
            if (!string.IsNullOrEmpty(_idApuracao))
            {
                produtos = Produtos.Where(p => p.Apuracao.Equals(_idApuracao));
            }
            else
            {
                produtos = Produtos;
            }

            return produtos.GroupBy(p => p.Cfop)
               .Select(p => new
               {
                   CFOP = p.Key,
                   VALOR_CONTABIL = p.Sum(pp => pp.ValorContabil),
                   BASE_CALCULO = p.Sum(pp => pp.BaseCalculo),
                   ICMS = p.Sum(pp => pp.Icms)
               });
        }

        public object ObterServicos()
        {
            return Servicos.GroupBy(s => s.Cfop)
                            .Select(s => new
                            {
                                CFOP = s.Key,
                                VALOR_CONTABIL = s.Sum(ss => ss.ValorContabil),
                                BASE_CALCULO = s.Sum(ss => ss.BaseCalculo),
                                ICMS = s.Sum(ss => ss.Icms)
                            });
        }
    }
}