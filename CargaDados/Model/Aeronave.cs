using System;
using System.Collections.Generic;
using System.Text;

namespace CargaDados.Model
{
    public class Aeronave
    {
        public string Nome { get; set; }
        public string Prefixo { get; set; }
        public int QuantidadePassageiro { get; set; }
        public int QuatidadeTripulante { get; set; }
        public bool Disponibilidade { get; set; }
        public string CaracteristicaBasica { get; set; }
        public string DetalhamentoBagagem { get; set; }
        public string DetalhamentoBagageiro { get; set; }
        public string Observacao { get; set; }
        public decimal Autonomia { get; set; }
        public decimal VelocidadeCruzeiro { get; set; }
        public decimal ValorMinimoOperacao { get; set; }
        public decimal LimiteCargaMin { get; set; }
        public decimal LimiteCargaMax { get; set; }
        public decimal PesoOperacional { get; set; }
        public decimal PesoVazio { get; set; }
        public decimal ValorKm { get; set; }
        public DateTime SituacaoAeronavegabil { get; set; }
        public string Serial { get; set; }

        public Guid Id_BaseAtual { get; set; }
        public bool ConfirmacaoBaseAtual { get; set; }

        public Guid Id_Operado { get; set; }
        public Guid Id_Modelo { get; set; }
        public Guid Id_Base { get; set; }
    }
}
