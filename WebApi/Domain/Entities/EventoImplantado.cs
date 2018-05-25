using System;

namespace Domain.Entities
{
    public class EventoImplantado
    {
        public int EventoImplantadoId { get; set; }
        public string Identificador { get; set; }
        public string IdentificadorCorrelacao { get; set; }
        public string IdentificadorNegocio { get; set; }
        public DateTime DataExecucaoEvento { get; set; }
        public CoberturaContratada cobertura { get; set; }
        public HistoricoCobertura historico { get; set; }

    }
}
