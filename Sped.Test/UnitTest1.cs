using Converter;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace Sped.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test2()
        {
            var pathSped = @"D:\OneDrive\Downloads\ENC__Maxtil_-_prodepe_Janeiro_2021\SpedEFD-07265878000106-032406223-Remessa de arquivo original-jan2021.txt";
            var pathFile = Path.Combine(Path.GetDirectoryName(pathSped), "APURACAO.xlsx");

            var lines = File.ReadAllLines(pathSped);

            var produtos = new Produtos(lines);
            var position = 1;
            JsonToXlsx produtosToXlsx = JsonConvert.SerializeObject(produtos);
            var wb = produtosToXlsx.Workbook;
            wb.Worksheet(position).Name = "PRODUTOS";

            var apuracoes = new string[] { "PE010302", "PE000100" };

            foreach (var idApuracao in apuracoes)
            {
                var apProdutos = new Apuracao(produtos, idApuracao);

                wb.AddWorksheet(apProdutos.ObterProdutos().GetIXLWorksheet(idApuracao));
            }
            var servicos = new Servicos(lines);

            wb.AddWorksheet(servicos.GetIXLWorksheet("SERVICOS"));

            var apServicos = new Apuracao(servicos);

            wb.AddWorksheet(apServicos.ObterServicos().GetIXLWorksheet("APURACAO_SERVICOS"));

            wb.Author = "Jairo Brito";
            wb.SaveAs(pathFile);
            var pathExel = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Microsoft Office\root\Office16\EXCEL.EXE");

            if (File.Exists(pathExel))
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo(pathFile)
                    {
                        CreateNoWindow = true,
                        FileName = pathExel,
                        Arguments = pathFile
                    }
                };
                process.Start();
            }

            Assert.True(File.Exists(pathFile));
        }
    }
}