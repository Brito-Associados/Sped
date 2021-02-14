using ClosedXML.Excel;
using Converter;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace Sped
{
    internal static class Program
    {
        public static void Main(string[] _)
        {
            string pathSped;
            do
            {
                Console.WriteLine("Insira o caminho válido completo do arquivo SPED a ser lido.");
                pathSped = Console.ReadLine().Replace(@"""", "");
            } while (!File.Exists(pathSped));

            string[] apuracoes;
            do
            {
                Console.WriteLine("Insira as os códigos das a purações que deseja obter separados por virgula.");
                Console.WriteLine("Ex: PE010302, PE000100");
                var aprs = Console.ReadLine().Replace(" ", "");
                apuracoes = aprs.Split(",");
            } while (apuracoes.Length.Equals(0));

            Console.Clear();
            Console.WriteLine("Processando...");

            var lines = File.ReadAllLines(pathSped);

            var produtos = new Produtos(lines);

            JsonToXlsx produtosToXlsx = JsonConvert.SerializeObject(produtos);
            var wb = produtosToXlsx.Workbook;
            wb.Worksheet(1).Name = "PRODUTOS";

            var apConsolidada = new Apuracao(produtos);
            var a = apConsolidada.ObterProdutosPorApuracao(); 
            wb.AddWorksheet(a.GetIXLWorksheet("CONSOLIDADA"));

            foreach (var idApuracao in apuracoes)
            {
                var apProdutos = new Apuracao(produtos, idApuracao);

                wb.AddWorksheet(apProdutos.ObterProdutosPorApuracao().GetIXLWorksheet(idApuracao));
            }

            var servicos = new Servicos(lines);

            if (servicos.Count > 0)
            {
                wb.AddWorksheet(servicos.GetIXLWorksheet("SERVICOS"));
            }

            var apServicos = new Apuracao(servicos);

            wb.AddWorksheet(apServicos.ObterServicos().GetIXLWorksheet("APURACAO_SERVICOS"));

            wb.Author = "Jairo Brito";

            var pathFile = Path.Combine(Path.GetDirectoryName(pathSped), "APURACAO.xlsx");
            wb.SaveAs(pathFile);

            var pathExel = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Microsoft Office\root\Office16\EXCEL.EXE");
            AbrirArquivo(pathFile, pathExel);
        }

        private static void AbrirArquivo(string pathFile, string pathExel)
        {
            if (File.Exists(pathExel) && File.Exists(pathFile))
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        CreateNoWindow = true,
                        FileName = pathExel,
                        Arguments = pathFile
                    }
                };
                process.Start();
            }
        }
    }

    public static class Extensions
    {
        public static IXLWorksheet GetIXLWorksheet(this object obj, string nameWorkSheet)
        {
            JsonToXlsx objToXlsx = JsonConvert.SerializeObject(obj);
            var ws = objToXlsx.Workbook.Worksheet(1);
            ws.Name = nameWorkSheet;

            return ws;
        }
    }
}