using Domain.Enum;
using System;

namespace Domain.Entities
{
    public class HistoricoCobertura
    {
        public int HistoricoCoberturaId { get; set; }                   
        public int EventoImplantadoId { get; set; }
        public PeriodicidadeEnum PeriodicidadeId { get; set; }
        public long ItemCertificadoApoliceId { get; set; }
        public DateTime DataNascimentoBeneficiario { get; set; }
        public string SexoBeneficiario { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
