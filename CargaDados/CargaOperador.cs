using CargaDados.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace CargaDados
{
    public class CargaOperador
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

                Operador operador = new Operador();

                int RAZAO_SOCIAL = 0;
                int CNPJ = 0;
                int TELEFONE = 0;
                int EMAIL = 0;
                int VALIDADE_OPERACIONAL = 0;
                int CELULAR = 0;
                int CEP = 0;

                for (int i = 1; i < worksheet.Dimension.Columns; i++)
                {
                    switch (worksheet.Cells[1, i].Value.ToString())
                    {
                        case "Razão Social":
                            RAZAO_SOCIAL = i;
                            break;
                        case "CNPJ":
                            CNPJ = i;
                            break;
                        case "Telefone":
                            TELEFONE = i;
                            break;
                        case "E-Mail":
                            EMAIL = i;
                            break;
                        case "Validade_Operacional":
                            VALIDADE_OPERACIONAL = i;
                            break;
                        case "CEP":
                            CEP = i;
                            break;
                        case "Celular":
                            CELULAR = i;
                            break;
                        default:
                            break;
                    }
                }


                for (int i = 2; i < worksheet.Dimension.Rows; i++)
                {
                    operador = new Operador
                    {
                        RazaoSocial = worksheet.Cells[i, RAZAO_SOCIAL].Value != null ? worksheet.Cells[i, RAZAO_SOCIAL].Value.ToString().TrimEnd().TrimStart() : null,
                        NomeFantasia = worksheet.Cells[i, RAZAO_SOCIAL].Value != null ? worksheet.Cells[i, RAZAO_SOCIAL].Value.ToString().TrimEnd().TrimStart() : null,
                        Email = worksheet.Cells[i, EMAIL].Value != null ? worksheet.Cells[i, EMAIL].Value.ToString().TrimEnd().TrimStart() : null,
                        Cnpj = worksheet.Cells[i, CNPJ].Value != null ? CnpjFormatado(worksheet.Cells[i, CNPJ].Value.ToString().TrimEnd().TrimStart()) : null,

                        ValidadeOperacional = worksheet.Cells[i, VALIDADE_OPERACIONAL].Value != null ?
                        Convert.ToDateTime(worksheet.Cells[i, VALIDADE_OPERACIONAL].Value.ToString().TrimEnd().TrimStart())
                        : DateTime.Parse("1/1/2008 00:01:01 AM", CultureInfo.InvariantCulture),

                        Telefone = worksheet.Cells[i, TELEFONE].Value != null ? TelefoneSemMascara(worksheet.Cells[i, TELEFONE].Value.ToString().TrimEnd().TrimStart()) : 0,
                        Celular = worksheet.Cells[i, CELULAR].Value != null ? TelefoneSemMascara(worksheet.Cells[i, CELULAR].Value.ToString().TrimEnd().TrimStart()) : 0,

                        Cep = worksheet.Cells[i, CEP].Value != null ? CepSemMascara(worksheet.Cells[i, CEP].Value.ToString().TrimEnd().TrimStart()) : 0,
                        Rua = GetEndereco(worksheet.Cells[i, CEP].Value != null ? CepSemMascara(worksheet.Cells[i, CEP].Value.ToString().TrimEnd().TrimStart()).ToString() : null).Rua,
                        Numero = GetEndereco(worksheet.Cells[i, CEP].Value != null ? CepSemMascara(worksheet.Cells[i, CEP].Value.ToString().TrimEnd().TrimStart()).ToString() : null).Numero,
                        Complemento = GetEndereco(worksheet.Cells[i, CEP].Value != null ? CepSemMascara(worksheet.Cells[i, CEP].Value.ToString().TrimEnd().TrimStart()).ToString() : null).Complemento,
                        Bairro = GetEndereco(worksheet.Cells[i, CEP].Value != null ? CepSemMascara(worksheet.Cells[i, CEP].Value.ToString().TrimEnd().TrimStart()).ToString() : null).Bairro,
                        Cidade = GetEndereco(worksheet.Cells[i, CEP].Value != null ? CepSemMascara(worksheet.Cells[i, CEP].Value.ToString().TrimEnd().TrimStart()).ToString() : null).Cidade,
                        Uf = GetEndereco(worksheet.Cells[i, CEP].Value != null ? CepSemMascara(worksheet.Cells[i, CEP].Value.ToString().TrimEnd().TrimStart()).ToString() : null).Uf,
                    };

                    ConectApi.conexao(operador, "/api/v1/Operado/cadastro");
                }
            }
        }

        public long CepSemMascara(string cep)
        {
            var cepSemPonto = cep.Replace(".", "");
            var ceptratado = cepSemPonto.Replace("-", "");

            return Int64.Parse(ceptratado);
        }

        public long TelefoneSemMascara(string telefone)
        {
            var telefoneSemChaves = telefone.Replace("(", "");
            var telefoneSemChaves2 = telefoneSemChaves.Replace(") ", "");
            var telefoneFormatado = telefoneSemChaves2.Replace("-", "");

            return Int64.Parse(telefoneFormatado);
        }

        public string CnpjFormatado(string cnpj)
        {
            var cnpjSemPonto = cnpj.Replace(".", "");
            var cnpjSemBarra = cnpjSemPonto.Replace("/", "");
            var CnpjFormatado = cnpjSemBarra.Replace("-", "");

            return CnpjFormatado;
        }

        public EnderecoModel GetEndereco(string cep)
        {
            return ConectApi.GetEndereco(cep).Result;
        }
    }
}