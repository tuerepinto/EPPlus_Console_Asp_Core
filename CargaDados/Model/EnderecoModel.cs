using System;
using System.Collections.Generic;
using System.Text;

namespace CargaDados.Model
{
    public class EnderecoModel
    {
        public long Cep { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Tipo_Logradouro { get; set; }
        public string Logradouro { get; set; }
        public int Resultado { get; set; }
        public string Resultado_text { get; set; }
        public Guid Id_Usuario { get; set; }
    }
}
