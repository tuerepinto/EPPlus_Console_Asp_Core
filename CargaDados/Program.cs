using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CargaDados
{
    class Program
    {
        private CargaAeroportos _cargaAeroportos;
        private CargaAeroportos CargaAeroportos
        {
            get { return _cargaAeroportos ?? (_cargaAeroportos = new CargaAeroportos()); }
        }

        private CargaOperador _cargaOperador;
        private CargaOperador CargaOperador
        {
            get { return _cargaOperador ?? (_cargaOperador = new CargaOperador()); }
        }

        private const string DiretorioAtual = @"C:\ArquivoCarga_xlsx";

        public static void Main(string[] args)
        {
            Program program = new Program();

            var confirmacao = string.Empty;
            var aba = 0;

            try
            {
                Console.WriteLine("Qual o tipo de carga?");
                Console.WriteLine(string.Format("1 - {0} \n\r2 - {1} \n\r3 - {2} \n\r4 - {3}", "Aeroportos", "Modelo Aeronave", "Operador", "Aeronaves"));
                var tipoCarga = Convert.ToInt32(Console.ReadLine());

                Console.Clear();

                Console.WriteLine("Qual o tipo de carga?");

                switch (tipoCarga)
                {
                    case 1:
                        Console.WriteLine(string.Format("{0} 1 - {1} \n\r{2} 2 - {3} \n\r{4} 3 - {5} \n\r{6} 4 - {7}", "*", "Aeroportos", "", "Modelo Aeronave", "", "Operador", "", "Aeronaves"));
                        break;
                    case 2:
                        Console.WriteLine(string.Format("{0} 1 - {1} \n\r{2} 2 - {3} \n\r{4} 3 - {5} \n\r{6} 4 - {7}", "", "Aeroportos", "*", "Modelo Aeronave", "", "Operador", "", "Aeronaves"));
                        break;
                    case 3:
                        Console.WriteLine(string.Format("{0} 1 - {1} \n\r{2} 2 - {3} \n\r{4} 3 - {5} \n\r{6} 4 - {7}", "", "Aeroportos", "", "Modelo Aeronave", "*", "Operador", "", "Aeronaves"));
                        break;
                    case 4:
                        Console.WriteLine(string.Format("{0} 1 - {1} \n\r{2} 2 - {3} \n\r{4} 3 - {5} \n\r{6} 4 - {7}", "", "Aeroportos", "", "Modelo Aeronave", "", "Operador", "*", "Aeronaves"));
                        break;
                    default:
                        break;
                }

                string[] filePaths = Directory.GetFiles(DiretorioAtual, "*.xlsx", SearchOption.AllDirectories);

                foreach (var item in filePaths)
                {
                    if (string.IsNullOrEmpty(confirmacao) || confirmacao.ToUpper().Equals("N"))
                    {
                        Console.WriteLine(string.Format("Confirma a carga usando ({0})? S|N", item));
                        confirmacao = Console.ReadLine();
                    }

                    if (confirmacao.ToUpper().Equals("S"))
                    {
                        Console.WriteLine("Digite a posição '0, 1 ou 2 ...' da aba:");
                        aba = Convert.ToInt32(Console.ReadLine());

                        FileInfo existingFile = new FileInfo(item);
                        program.Start(existingFile, aba, tipoCarga);

                        break;

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

        private void Start(FileInfo diretorioExcel, int aba, int tipoCarga)
        {
            switch (tipoCarga)
            {
                case 1:
                    CargaAeroportos.Read(diretorioExcel, aba);
                    break;
                case 2:
                    break;
                case 3:
                    CargaOperador.Read(diretorioExcel, aba);
                    break;
                case 4:
                    break;
                default:
                    break;
            }

        }
    }
}
