using System;
using System.Collections.Generic;
using System.Text;

namespace CargaDados.Model
{
    public class Aeroportos
    {
        public string NomePista { get; set; }
        public string Utilizacao { get; set; }
        public string Categoria { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public DateTime HoraIniFuncionamento { get; set; }
        public DateTime HoraFimFuncionamento { get; set; }
        public string Observacao { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LocalidadeInfraero { get; set; }
        public decimal LatDec { get; set; }
        public decimal LongDec { get; set; }

    }
}
