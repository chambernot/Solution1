using System;
using Mongeral.Infrastructure.Assertions;
using Mongeral.Infrastructure.Domain.Model;

namespace Mongeral.Provisao.V2.Domain
{
    public abstract class EventoOperacional : Entidade<Guid>
    {     
        protected EventoOperacional(Guid identificador,string idCorrelacao, string idNegocio, DateTime dataExecucao)
        {
            Assertion.GreaterThan(identificador, default(Guid), "O Identificador do evento não foi informado.").Validate();
            
            Identificador = identificador;
            IdentificadorCorrelacao = idCorrelacao;
            IdentificadorNegocio = idNegocio;
            DataExecucaoEvento = dataExecucao;
            DataCriacaoEvento = DateTime.Now;
        }

        public abstract short TipoEventoId { get;  }        
        public Guid Identificador { get; protected set; }
        public string IdentificadorCorrelacao { get; protected set; }
        public string IdentificadorNegocio { get; protected set; }
        public DateTime DataExecucaoEvento { get; protected set; }
        public DateTime DataCriacaoEvento { get; protected set; }        
        
    }
}
