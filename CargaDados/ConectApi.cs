using CargaDados.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CargaDados
{
    public class ConectApi
    {
        private const string access_token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIwZGU0OWVlZC1iN2U1LTQ1OTEtODQ1MS03ZWIxZjI0MTRhOGYiLCJlbWFpbCI6ImZseWFkYW1AZmx5YWRhbS5jb20uYnIiLCJ1bmlxdWVfbmFtZSI6ImZseWFkYW1AZmx5YWRhbS5jb20uYnIiLCJqdGkiOiJhZTI5YzlkMi01ODkyLTQwYmYtOTFiYy03ZGIyYTM5MDgyZmUiLCJuYmYiOjE1Njc4MDg3NDYsImlhdCI6MTU2NzgwODc0NiwiZXhwIjoxNTY3ODE1OTQ2LCJpc3MiOiJBREFNIENvbnRyb2xlIE9wZXJhY2lvbmFsIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3QifQ.z0M641CkXwzSCqsAyckTfQNIhrFYk_jlMHKmfId1kXk";
        private const string CEP_FORMART = "\\d{5}-\\d{3}";
        private const string URL = "http://cep.republicavirtual.com.br/web_cep.php?cep={0}&formato=json";

        private LogTxt _logTxt;
        private LogTxt LogTxt
        {
            get { return _logTxt ?? (_logTxt = new LogTxt()); }
        }

        public void conexao(object obj, string linkApi)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                Console.WriteLine(json + "\n\r");

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:5001/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json-patch+json"));

                var contentString = new StringContent(json, Encoding.UTF8, "application/json-patch+json");
                contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json-patch+json");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

                HttpResponseMessage response = client.PostAsync(linkApi, contentString).Result;
                Console.WriteLine(response + "\n\r");

                LogTxt.CriarLogTexto(Convert.ToString(response), "Carga");
            }
            catch (Exception ex)
            {
                LogTxt.CriarLogTexto(ex.Message, "Carga");
                throw new AggregateException(ex.Message);
            }
        }

        public bool ValidarCep(string cep)
        {
            return Regex.IsMatch(cep, (CEP_FORMART));
        }

        public async Task<EnderecoModel> GetEndereco(string cep)
        {
            EnderecoModel endereco;

            var cepReplace = cep;

            if (Regex.IsMatch(cep, (CEP_FORMART)))
            {
                cepReplace = cep.Replace("-", "");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);

                var response = await client.GetStringAsync(string.Format(URL, cepReplace));

                endereco = JsonConvert.DeserializeObject<EnderecoModel>(response);
            }

            return endereco;
        }
    }

}
