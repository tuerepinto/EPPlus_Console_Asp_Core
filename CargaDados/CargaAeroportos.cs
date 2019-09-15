using CargaDados.Model;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CargaDados
{
    public class CargaAeroportos
    {
        private ConectApi _conectApi;
        private ConectApi ConectApi
        {
            get { return _conectApi ?? (_conectApi = new ConectApi()); }
        }

        public void Read(FileInfo excelLocal, int aba)
        {
            using (ExcelPackage package = new ExcelPackage(excelLocal))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[aba];

                Aeroportos aeroporto = new Aeroportos();

                int IND_NOME_PISTA = 0;
                int IND_UTILIZACAO = 0;
                int IND_CATEGORIA = 0;
                int IND_CIDADE = 0;
                int IND_ESTADO = 0;
                int IND_PAIS = 0;
                int IND_OBS = 0;
                int IND_LATITUDE = 0;
                int IND_LONGITUDE = 0;
                int IND_LOCALIDADE = 0;
                int IND_LAT_DEC = 0;
                int IND_LONG_DEC = 0;

                for (int i = 1; i < worksheet.Dimension.Columns; i++)
                {
                    switch (worksheet.Cells[1, i].Value.ToString())
                    {
                        case "IND_NOME_PISTA":
                            IND_NOME_PISTA = i;
                            break;
                        case "IND_UTILIZACAO":
                            IND_UTILIZACAO = i;
                            break;
                        case "IND_CATEGORIA":
                            IND_CATEGORIA = i;
                            break;
                        case "IND_CIDADE":
                            IND_CIDADE = i;
                            break;
                        case "IND_ESTADO":
                            IND_ESTADO = i;
                            break;
                        case "IND_PAIS":
                            IND_PAIS = i;
                            break;
                        case "IND_OBS":
                            IND_OBS = i;
                            break;
                        case "IND_LATITUDE":
                            IND_LATITUDE = i;
                            break;
                        case "IND_LONGITUDE":
                            IND_LONGITUDE = i;
                            break;
                        case "IND_LOCALIDADE":
                            IND_LOCALIDADE = i;
                            break;
                        case "IND_LAT_DEC":
                            IND_LAT_DEC = i;
                            break;
                        case "IND_LONG_DEC":
                            IND_LONG_DEC = i;
                            break;
                        default:
                            break;
                    }
                }

                for (int i = 2; i < worksheet.Dimension.Rows; i++)
                {
                    aeroporto = new Aeroportos
                    {
                        NomePista = worksheet.Cells[i, IND_NOME_PISTA].Value != null ? worksheet.Cells[i, IND_NOME_PISTA].Value.ToString().TrimEnd().TrimStart() : null,
                        Utilizacao = worksheet.Cells[i, IND_UTILIZACAO].Value != null ? worksheet.Cells[i, IND_UTILIZACAO].Value.ToString() : null,
                        Categoria = worksheet.Cells[i, IND_CATEGORIA].Value != null ? worksheet.Cells[i, IND_CATEGORIA].Value.ToString() : null,
                        Cidade = worksheet.Cells[i, IND_CIDADE].Value != null ? worksheet.Cells[i, IND_CIDADE].Value.ToString() : null,
                        Estado = worksheet.Cells[i, IND_ESTADO].Value != null ? worksheet.Cells[i, IND_ESTADO].Value.ToString() : null,
                        Pais = worksheet.Cells[i, IND_PAIS].Value != null ? worksheet.Cells[i, IND_PAIS].Value.ToString() : null,
                        Observacao = worksheet.Cells[i, IND_OBS].Value != null ? worksheet.Cells[i, IND_OBS].Value.ToString() : null,
                        Latitude = worksheet.Cells[i, IND_LATITUDE].Value != null ? worksheet.Cells[i, IND_LATITUDE].Value.ToString() : null,
                        Longitude = worksheet.Cells[i, IND_LONGITUDE].Value != null ? worksheet.Cells[i, IND_LONGITUDE].Value.ToString() : null,
                        LocalidadeInfraero = worksheet.Cells[i, IND_LOCALIDADE].Value != null ? worksheet.Cells[i, IND_LOCALIDADE].Value.ToString() : null,
                        LatDec = Convert.ToDecimal(worksheet.Cells[i, IND_LAT_DEC].Value),
                        LongDec = Convert.ToDecimal(worksheet.Cells[i, IND_LONG_DEC].Value),
                        HoraIniFuncionamento = DateTime.Parse("1/1/2008 00:01:01 AM",
                          System.Globalization.CultureInfo.InvariantCulture),
                        HoraFimFuncionamento = DateTime.Parse("1/1/2008 11:59:59 PM",
                          System.Globalization.CultureInfo.InvariantCulture)
                    };

                    ConectApi.conexao(aeroporto, "/api/v1/Aeronave");
                }
            }
        }
    }
}
