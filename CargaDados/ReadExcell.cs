using CargaDados.Model;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace CargaDados
{
    public class ReadExcell
    {
        private const string access_token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6WyJiYzViMGU5Zi1jNGQ5LTQxNDQtOGEzYi0xYWM5NjFlMDhlNDYiLCJiYzViMGU5Zi1jNGQ5LTQxNDQtOGEzYi0xYWM5NjFlMDhlNDYiXSwianRpIjoiNmJlZmVmMTE5M2IwNGYwNWE2NGQyNWM1Y2FjZjVjZDUiLCJuYmYiOjE1NjM5OTkxMTIsImV4cCI6MTU2NDAwNjMxMiwiaWF0IjoxNTYzOTk5MTEyLCJpc3MiOiJBREFNIENvbnRyb2xlIE9wZXJhY2lvbmFsIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3QifQ.ugh_5K53urT8fhgiGaB1e0kxtTdhfPukLXprmwj9ui0";

        public async Task Read(FileInfo excelLocal, int aba)
        {
            using (ExcelPackage package = new ExcelPackage(excelLocal))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[aba];

                var aeroporto = new Aeroportos();
                int IND_NOME_PISTA = 0;
                int IND_UTILIZACAO = 0;
                int IND_CATEGORIA = 0;
                int IND_CIDADE = 0;
                int IND_ESTADO = 0;
                int IND_PAIS = 0;
                int IND_OBS = 0;
                int IND_LATITUDE = 0;
                int IND_LONGITUDE = 0;
                int IND_LOCALIDADE_INFRAERO = 0;
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
                        case "IND_LOCALIDADE_INFRAERO":
                            IND_LOCALIDADE_INFRAERO = i;
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
                        NomePista = worksheet.Cells[i, IND_NOME_PISTA].Value != null ? worksheet.Cells[i, IND_NOME_PISTA].Value.ToString() : string.Empty,
                        Utilizacao = worksheet.Cells[i, IND_UTILIZACAO].Value != null ? worksheet.Cells[i, IND_UTILIZACAO].Value.ToString() : string.Empty,
                        Categoria = worksheet.Cells[i, IND_CATEGORIA].Value != null ? worksheet.Cells[i, IND_CATEGORIA].Value.ToString() : string.Empty,
                        Cidade = worksheet.Cells[i, IND_CIDADE].Value != null ? worksheet.Cells[i, IND_CIDADE].Value.ToString() : string.Empty,
                        Estado = worksheet.Cells[i, IND_ESTADO].Value != null ? worksheet.Cells[i, IND_ESTADO].Value.ToString() : string.Empty,
                        Pais = worksheet.Cells[i, IND_PAIS].Value != null ? worksheet.Cells[i, IND_PAIS].Value.ToString() : string.Empty,
                        Observacao = worksheet.Cells[i, IND_OBS].Value != null ? worksheet.Cells[i, IND_OBS].Value.ToString() : string.Empty,
                        Latitude = worksheet.Cells[i, IND_LATITUDE].Value != null ? worksheet.Cells[i, IND_LATITUDE].Value.ToString() : string.Empty,
                        Longitude = worksheet.Cells[i, IND_LONGITUDE].Value != null ? worksheet.Cells[i, IND_LONGITUDE].Value.ToString() : string.Empty,
                        LocalidadeInfraero = worksheet.Cells[i, IND_LOCALIDADE_INFRAERO].Value != null ? worksheet.Cells[i, IND_LOCALIDADE_INFRAERO].Value.ToString() : string.Empty,
                        LatDec = Convert.ToDecimal(worksheet.Cells[i, IND_LAT_DEC].Value),
                        LongDec = Convert.ToDecimal(worksheet.Cells[i, IND_LONG_DEC].Value),
                        HoraIniFuncionamento = DateTime.Parse("1/1/2008 00:01:01 AM",
                          System.Globalization.CultureInfo.InvariantCulture),
                        HoraFimFuncionamento = DateTime.Parse("1/1/2008 11:59:59 PM",
                          System.Globalization.CultureInfo.InvariantCulture)
                    };

                    try
                    {
                        var json = JsonConvert.SerializeObject(aeroporto);
                        Console.WriteLine(json + "\n\r");

                        HttpClient client = new HttpClient();
                        client.BaseAddress = new Uri("https://apiadam.azurewebsites.net/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json-patch+json"));

                        var contentString = new StringContent(json, Encoding.UTF8, "application/json-patch+json");
                        contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                        HttpResponseMessage response = client.PostAsync("api/Aeroporto", contentString).Result;
                        Console.WriteLine(response + "\n\r");                        
                    }
                    catch (Exception ex)
                    {
                        throw new AggregateException(ex.Message);
                    }

                }
            }
        }
    }
}
