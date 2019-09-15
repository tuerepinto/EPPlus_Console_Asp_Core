using System;
using System.Collections.Generic;
using System.Text;

namespace CargaDados.Model
{
    public class Operador
    {
        public string Email { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public DateTime ValidadeOperacional { get; set; }
        public long Telefone { get; set; }
        public long Celular { get; set; }
        public long Cep { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
    }
}
