using System;
using System.Collections.Generic;
using System.Text;

namespace CargaDados.Model
{
    public class ModeloAeronave
    {
        public string Fabricante { get; set; }

        public string Modelo { get; set; }

        public int Ano { get; set; }

        public decimal ValorSugerido { get; set; }
    }
}
