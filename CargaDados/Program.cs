using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CargaDados
{
    class Program
    {
        private ReadExcell _readExcell;
        private ReadExcell ReadExcell
        {
            get { return _readExcell ?? (_readExcell = new ReadExcell()); }
        }

        private const string DiretorioAtual = @"C:\Users\Framework\Downloads";

        public static void Main(string[] args)
        {
            Program program = new Program();

            Console.WriteLine("Digite o nome do Excel:");
            var name = Console.ReadLine();

            string[] filePaths = Directory.GetFiles(DiretorioAtual, "*.xlsx", SearchOption.AllDirectories);


            Console.WriteLine("Digite o nome da aba.");
            var aba = Convert.ToInt32(Console.ReadLine());
            try
            {
                foreach (var item in filePaths)
                {
                    var fileName = item.Replace(DiretorioAtual, "");
                    var newName = fileName.Replace("\\", "");

                    if (newName.Equals(string.Concat(name, ".xlsx")))
                    {
                        FileInfo existingFile = new FileInfo(item);
                        program.Start(existingFile, aba).Wait();
                    }
                }

                Console.WriteLine("Fim da operação.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }

        private async Task Start(FileInfo diretorioExcel, int aba)
        {
            await ReadExcell.Read(diretorioExcel, aba);
        }
    }
}
